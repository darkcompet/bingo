using App;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// [Service]
var services = builder.Services;

// Add services to the container.
services.AddRazorPages();

// Config database connections
services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer("Data Source=18.143.29.66,1433;Initial Catalog=bingo;User ID=sa;Password=Staging1234!"));


// [App]
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
