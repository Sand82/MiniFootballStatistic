using System.ComponentModel.DataAnnotations;

using static MiniFootballStatistic.Data.DataConstants;

namespace MiniFootballStatistic.Models.Team
{
    public class GetGoalsEditModel
    {
        public int TournamentId { get; set; }

        public int TeamId { get; set; }

        [Range(MinTeamStatsValue, MaxTeamStatsValue)]
        public int Goals { get; set; }

        public int GroupNumber { get; set; }

        public int SchemaLength { get; set; }
    }
}
