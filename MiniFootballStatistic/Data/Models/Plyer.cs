using System.ComponentModel.DataAnnotations;

using static MiniFootballStatistic.Data.DataConstants;

namespace MiniFootballStatistic.Data.Models
{
    public class Plyer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(MaxTournamentTypeNameLength)]
        public string? Name { get; set; }

        [Range(MinPlayerStatsValue, MaxPlayerStatsValue)]
        public int Goals { get; set; }

        [Range(MinPlayerStatsValue, MaxPlayerStatsValue)]
        public int Assists { get; set; }

        [Range(MinPlayerStatsValue, MaxPlayerStatsValue)]
        public int MachesCount { get; set; }

        public int TeamId { get; set; }

        public Team? Teams { get; set; }
    }
}
