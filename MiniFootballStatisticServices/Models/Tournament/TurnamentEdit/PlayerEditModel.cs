using System.ComponentModel.DataAnnotations;

using static MiniFootballStatisticData.Data.DataConstants;

namespace MiniFootballStatisticServices.Models.Tournament.TournamentEdit
{
    public class PlayerEditModel
    {
        [Required]
        [Display(Name = "Player Name")]
        public string Name { get; set; }

        public int? TeamId { get; set; }

        [Range(MinPlayerStatsValue, MaxPlayerStatsValue,
            ErrorMessage = "The field {0} is not valid! Must be between of {2} and {1} symbols.")]
        public int Goals { get; set; }

        [Range(MinPlayerStatsValue, MaxPlayerStatsValue,
            ErrorMessage = "The field {0} is not valid! Must be between of {2} and {1} symbols.")]
        public int Assists { get; set; }


    }
}
