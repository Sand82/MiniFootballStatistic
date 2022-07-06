using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using MiniFootballStatisticData.Data.Models;

namespace MiniFootballStatisticData.Data
{
    public class FoodballStatisticDbContext : IdentityDbContext
    {
        public FoodballStatisticDbContext(DbContextOptions<FoodballStatisticDbContext> options)
            : base(options)
        {            
        }

       public DbSet<TournamentCategory> TournamentCategories { get; set; }

       public DbSet<Schema> Schemas { get; set; }

       public DbSet<Tournament> Tournaments { get; set; }

       public DbSet<Team> Team { get; set; }

       public DbSet<Player> Player { get; set; }
    }
}