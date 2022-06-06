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

        [Required]
        [StringLength(MaxTeamPositionLength)]
        public string? TournamentPosition { get; set; }

        [Range(MinTeamStatsValue, MaxTeamStatsValue)]
        public int? ScoredGoals { get; set; }

        [Range(MinTeamStatsValue, MaxTeamStatsValue)]
        public int? AccumolateGoals { get; set; }

        [Range(MinTeamStatsValue, MaxTeamStatsValue)]
        public int? Difference { get; set; }

        public int TeamId { get; set; }

        public Team? Teams { get; set; }

        public bool? IsLose { get; set; }

        public IList<Plyer>? Players { get; set; }

    }
}
