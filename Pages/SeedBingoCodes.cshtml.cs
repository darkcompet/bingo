using App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bingo.Pages;

public class SeedBingoCodes : PageModel {
	private readonly ILogger<SeedBingoCodes> logger;
	private readonly AppDbContext dbContext;

	public SeedBingoCodes(ILogger<SeedBingoCodes> logger, AppDbContext dbContext) {
		this.logger = logger;
		this.dbContext = dbContext;
	}

	public int generatedBingoCodeCount { get; private set; }

	public async Task<IActionResult> OnGet([FromQuery] int fromCode, [FromQuery] int toCode) {
		var bingoCode = toCode;
		while (bingoCode-- >= fromCode) {
			this.dbContext.bingos.Attach(new BingoModel() {
				code = bingoCode.ToString()
			});
			++generatedBingoCodeCount;
		}
		await this.dbContext.SaveChangesAsync();

		return Page();
	}
}
