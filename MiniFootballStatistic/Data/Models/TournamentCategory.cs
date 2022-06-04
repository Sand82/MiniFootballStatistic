using System.ComponentModel.DataAnnotations;

namespace MiniFootballStatistic.Data.Models
{
    public class TournamentCategory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
