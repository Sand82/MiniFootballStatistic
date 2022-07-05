using System.ComponentModel.DataAnnotations;

using static MiniFootballStatistic.Data.DataConstants;

namespace MiniFootballStatistic.Models.Team
{
    public class GetNameEditModel
    {
        public int TournamentId { get; set; }

        public int TeamId { get; set; }

        [Required]        
        [StringLength(MaxNameLenght, MinimumLength = MinNameLenght,
          ErrorMessage = "The field {0} is not valid! Must be between of {2} and {1} symbols.")]
        public string? TeamName { get; set; }
    }
}
