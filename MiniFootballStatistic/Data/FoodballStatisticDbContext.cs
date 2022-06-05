using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniFootballStatistic.Data.Models;

namespace MiniFootballStatistic.Data
{
    public class FoodballStatisticDbContext : IdentityDbContext
    {
        public FoodballStatisticDbContext(DbContextOptions<FoodballStatisticDbContext> options)
            : base(options)
        {            
        }

       public DbSet<TournamentCategory> TournamentCategories { get; set; }

       public DbSet<Schema> Schemas { get; set; }


    }
}