using System.ComponentModel.DataAnnotations;

using static MiniFootballStatistic.Data.DataConstants;

namespace MiniFootballStatistic.Data.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(MaxTournamentTypeNameLength)]
        public string? Name { get; set; }

        [Range(MinTeamStatsValue, MaxTeamStatsValue)]
        public int? ScoredGoals { get; set; }

        [Range(MinTeamStatsValue, MaxTeamStatsValue)]
        public int? AccumolateGoals { get; set; }

        [Range(MinTeamStatsValue, MaxTeamStatsValue)]
        public int? Difference { get; set; }

        public int TournamentId { get; set; }

        public Tournament? Tournament { get; set; }

        public ICollection<Plyer>? Players { get; set; }

    }
}
