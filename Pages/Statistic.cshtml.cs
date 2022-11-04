using App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace bingo.Pages;

public class StatisticModel : PageModel {
	private readonly ILogger<StatisticModel> logger;
	private readonly AppDbContext dbContext;

	public StatisticModel(ILogger<StatisticModel> logger, AppDbContext dbContext) {
		this.logger = logger;
		this.dbContext = dbContext;
	}

	public int candidateCount { get; private set; }
	public int bingoCodeCount { get; private set; }

	public async Task<IActionResult> OnGet() {
		this.candidateCount = await this.dbContext.users.CountAsync();
		this.bingoCodeCount = await this.dbContext.bingos.CountAsync();

		return Page();
	}
}
