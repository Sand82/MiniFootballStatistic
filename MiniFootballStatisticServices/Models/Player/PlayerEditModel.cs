using System.ComponentModel.DataAnnotations;

using static MiniFootballStatisticData.Data.DataConstants;

namespace MiniFootballStatisticServices.Models.Player
{
    public class PlayerEditModel
    {
        public int Id { get; set; }

        public int TeamId { get; set; }

        [Required]
        [Display(Name = "Player Name")]
        [StringLength(MaxNameLenght, MinimumLength = MinNameLenght,
            ErrorMessage = "The field {0} is not valid! Must be between of {2} and {1} symbols.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Player Goals")]
        [Range(MinPlayerStatsValue, MaxPlayerStatsValue,
            ErrorMessage = "The field {0} is not valid! Must be between of {2} and {1} symbols.")]
        public int Goals { get; set; }

        [Required]
        [Display(Name = "Player Assists")]
        [Range(MinPlayerStatsValue, MaxPlayerStatsValue,
            ErrorMessage = "The field {0} is not valid! Must be between of {2} and {1} symbols.")]
        public int Assists { get; set; }        
    }
}
