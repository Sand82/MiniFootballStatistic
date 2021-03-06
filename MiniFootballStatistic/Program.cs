using MiniFootballStatisticData.Data;
using MiniFootballStatistic.Infrastructure;
using MiniFootballStatisticServices.Services.Home;
using MiniFootballStatisticServices.Services.Tournaments;
using MiniFootballStatisticServices.Services.Events;
using MiniFootballStatisticServices.Services.Api;
using MiniFootballStatisticServices.Services.Players;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FoodballStatisticDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    //options.Lockout.MaxFailedAccessAttempts = 5;
    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    //options.User.RequireUniqueEmail = true;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<FoodballStatisticDbContext>();

builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-ANTIF-TOKEN";
});

builder.Services.AddTransient<IHomeService, HomeService>();
builder.Services.AddTransient<ITournamentService, TournamentService>();
builder.Services.AddTransient<IEventService, EventService>();
builder.Services.AddTransient<IApiService, ApiService>();
builder.Services.AddTransient<IPlayerService, PlayerService>();

var app = builder.Build();

app.PrepareDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
