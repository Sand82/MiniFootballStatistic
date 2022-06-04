namespace MiniFootballStatistic.Infrastructure
{
    using MiniFootballStatistic.Data;
    using MiniFootballStatistic.Data.Models;

    using Microsoft.EntityFrameworkCore;

    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder PrepareDatabase(
           this IApplicationBuilder app)
        {
            var scolpedServices = app.ApplicationServices.CreateScope();
            var serviceProvider = scolpedServices.ServiceProvider;

            var data = serviceProvider.GetRequiredService<FoodballStatisticDbContext>();

            //var webHostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

            data.Database.Migrate();

            SeedCategories(data);

            //SeedAdministrator(serviceProvider);            

            return app;
        }

        private static void SeedAdministrator(IServiceProvider service)
        {
            //var userMager = service.GetRequiredService<UserManager<IdentityUser>>();
            //var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            //Task
            //    .Run(async () =>
            //    {
            //        if (await roleManager.RoleExistsAsync(AdministratorRoleName))
            //        {
            //            return;
            //        }

            //        var role = new IdentityRole { Name = AdministratorRoleName };

            //        await roleManager.CreateAsync(role);

            //        var author = new IdentityUser
            //        {
            //            Email = "",
            //            UserName = "",
            //        };

            //        await userMager.CreateAsync(author, "123456");

            //        await userMager.AddToRoleAsync(author, role.Name);
            //    })
            //    .GetAwaiter()
            //    .GetResult();
        }

        private static void SeedCategories(FoodballStatisticDbContext data)
        {
            if (data.TournamentCategories.Any())
            {
                return;
            }

            data.TournamentCategories.AddRange(new[]
            {
                new TournamentCategory { Name = "Championship" },
                new TournamentCategory { Name = "Tournament" },                
            });

            data.SaveChanges();
        }    
    }
}

