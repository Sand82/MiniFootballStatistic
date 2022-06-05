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
            SeedSchema(data);

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
                new TournamentCategory {
                    Name = "Tournament",
                    ImageUrl="https://media.istockphoto.com/vectors/first-prize-gold-trophy-iconprize-gold-trophy-winner-first-prize-vector-id1183252990?k=20&m=1183252990&s=612x612&w=0&h=BNbDi4XxEy8rYBRhxDl3c_bFyALnUUcsKDEB5EfW2TY=",
                    Descrioption = "A knockout tournament or elimination tournament is divided into successive rounds; each competitor plays in at least one fixture per round. The top-ranked competitors in each fixture progress to the next round. As rounds progress, the number of competitors and fixtures decreases. The final round, usually known as the final or cup final, consists of just one fixture; the winner of which is the overall champion."
                },
                new TournamentCategory {
                    Name = "Championship",
                    ImageUrl = "https://media.istockphoto.com/vectors/sport-tournament-label-vector-id864519464?k=20&m=864519464&s=612x612&w=0&h=R9yvGJ7ZcAoN_OuTr6bO0p42EOgBjmDxK7kjpu2unL8=",
                    Descrioption = "Championships in football, use a league system in which all competitors in the league play each other, either once or a number of times. This is also known as a round robin system.",
                },

            });

            data.SaveChanges();
        }

        private static void SeedSchema(FoodballStatisticDbContext data)
        {
            if (data.Schemas.Any())
            {
                return;
            }

            data.Schemas.AddRange(new[]
            {
                new Schema { 
                    Name = "Eight players bracket",
                    PositionCount = 8,
                    ImageUrl = "https://cdn4.vectorstock.com/i/thumb-large/18/48/tournament-bracket-template-color-championship-vector-41931848.jpg"
                },
                new Schema 
                {
                    Name = "Four players bracket",
                    PositionCount = 4,
                    ImageUrl = "https://cdn5.vectorstock.com/i/thumb-large/32/99/tournament-bracket-icon-suitable-for-football-vector-42313299.jpg"
                },
            });

            data.SaveChanges();
        }
    }
}

