using System.ComponentModel.DataAnnotations;

using static MiniFootballStatisticData.Data.DataConstants;

namespace MiniFootballStatisticServices.Models.Team
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
