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
        public int TournamentPosition { get; set; }

        [Range(MinTeamStatsValue, MaxTeamStatsValue)]
        public int? PositionResult { get; set; }

        [Range(MinTeamStatsValue, MaxTeamStatsValue)]
        public int? ScoredGoals { get; set; }

        [Range(MinTeamStatsValue, MaxTeamStatsValue)]
        public int? AccumulateGoals { get; set; }       

        public int TournamentId { get; set; }

        public Tournament? Tournaments { get; set; }

        public bool? IsLose { get; set; }       

        public bool? IsChampion { get; set; }

        public List<Player>? Players { get; set; }

    }
}
