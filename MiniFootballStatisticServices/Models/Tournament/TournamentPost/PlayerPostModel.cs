using System.ComponentModel.DataAnnotations;

using static MiniFootballStatisticData.Data.DataConstants;

namespace MiniFootballStatisticServices.Models.Tournament.TournamentPost

{
    public class PlayerPostModel
    {
        [Required]
        [Display(Name = "Player Name")]
        [StringLength(MaxNameLenght, MinimumLength = MinNameLenght,
            ErrorMessage = "The field {0} is not valid! Must be between of {2} and {1} symbols.")]
        public string? Name { get; set; }

        public int? TeamId { get; set; }

    }
}
