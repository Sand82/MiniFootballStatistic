using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MiniFootballStatistic.Data
{
    public class FoodballStatistic : IdentityDbContext
    {
        public FoodballStatistic(DbContextOptions<FoodballStatistic> options)
            : base(options)
        {
        }
    }
}