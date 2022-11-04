using App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace bingo.Pages;

public class NextWinner : PageModel {
	private readonly ILogger<NextWinner> logger;
	private readonly AppDbContext dbContext;

	public NextWinner(ILogger<NextWinner> logger, AppDbContext dbContext) {
		this.logger = logger;
		this.dbContext = dbContext;
	}

	public string? winnerBingoCode { get; private set; }

	public async Task<IActionResult> OnGet() {
		var nextWinner = await this.dbContext.users
			.Where(m => m.winner_choosed_at == null)
			.OrderBy(m => Guid.NewGuid())
			.FirstOrDefaultAsync()
		;

		if (nextWinner is null) {
			return Page();
		}

		nextWinner.winner_choosed_at = DateTime.UtcNow;

		await this.dbContext.SaveChangesAsync();

		// Update UI
		this.winnerBingoCode = nextWinner.bingo_code;

		return Page();
	}
}
