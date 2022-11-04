using System.Transactions;
using App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace bingo.Pages;

public class IndexModel : PageModel {
	private readonly ILogger<IndexModel> logger;
	private readonly AppDbContext dbContext;

	public IndexModel(ILogger<IndexModel> logger, AppDbContext dbContext) {
		this.logger = logger;
		this.dbContext = dbContext;
	}

	public string? bingoCode { get; private set; }

	public async Task<IActionResult> OnGet() {
		const string uid_key = "_dkuid";
		var uid = this.Request.Cookies.FirstOrDefault(m => m.Key == uid_key).Value;
		if (uid is null || uid.Trim().Length == 0) {
			uid = Guid.NewGuid().ToString("N");

			Console.WriteLine($"Generated new uid: {uid}");

			this.Response.Cookies.Append(uid_key, uid);
		}

		var user = await this.dbContext.users.FirstOrDefaultAsync(m => m.uid == uid);
		if (user is null) {
			user = await this.CreateNewUserWithBingoCodeRecursively(uid);

			if (user is null) {
				return Page();
			}
		}

		// Update UI
		this.bingoCode = user.bingo_code;

		return Page();
	}

	private async Task<UserModel?> CreateNewUserWithBingoCodeRecursively(string uid, int tryCount = 0) {
		Console.WriteLine($"CreateNewUserWithBingoCodeRecursively, tryCount: {tryCount}");

		if (tryCount > 10) {
			return null;
		}

		using (var txScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled)) {
			try {
				// Lock a bingo
				var rnd_bingo_id = await this.dbContext.bingos
					.Where(m => m.is_used == false)
					.Select(m => m.id)
					.OrderBy(m => Guid.NewGuid())
					.FirstOrDefaultAsync()
				;
				var bingo = await this.dbContext.bingos
					.FromSqlRaw($"SELECT * FROM [{DbConst.table_bingo}] WITH (UPDLOCK) WHERE [id] = {{0}} and [is_used] = {{1}}", rnd_bingo_id, false)
					.FirstOrDefaultAsync();

				if (bingo is null || bingo.is_used) {
					return await CreateNewUserWithBingoCodeRecursively(uid, tryCount + 1);
				}

				bingo.is_used = true;

				var newUser = new UserModel() {
					uid = uid,
					bingo_code = bingo.code
				};

				this.dbContext.users.Attach(newUser);

				await this.dbContext.SaveChangesAsync();
				txScope.Complete();

				return newUser;
			}
			catch (Exception e) {
				Console.WriteLine($"Could not create new user, tryCount: {tryCount}, error: {e.Message}");

				// Try more
				return await CreateNewUserWithBingoCodeRecursively(uid, tryCount + 1);
			}
		}
	}
}
