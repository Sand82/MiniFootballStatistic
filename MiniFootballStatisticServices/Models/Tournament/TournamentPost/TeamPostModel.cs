using System.ComponentModel.DataAnnotations;

using static MiniFootballStatisticData.Data.DataConstants;

namespace MiniFootballStatisticServices.Models.Tournament.TournamentPost
{
    public class TeamPostModel
    {
        public TeamPostModel()
        {
            this.Players = new List<PlayerPostModel>();
        }

        [Required]
        [Display(Name = "Team Name")]
        [StringLength(MaxNameLenght, MinimumLength = MinNameLenght,
            ErrorMessage = "The field {0} is not valid! Must be between of {2} and {1} symbols.")]        
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Tournament Position")]
        [Range(MinTeamStatsValue, MaxTeamStatsValue,
            ErrorMessage = "The field {0} is not valid! Must be between of {2} and {1} symbols.")]
        public int TournamentPosition { get; set; }

        public List<PlayerPostModel>? Players { get; set; }
    }
}
