using MiniFootballStatistic.Models.Team;
using System.ComponentModel.DataAnnotations;

using static MiniFootballStatistic.Data.DataConstants;

namespace MiniFootballStatistic.Models.Tournament
{
    public class TournamentPostModel
    {
        [Required]
        [StringLength(MaxNameLenght, MinimumLength = MinNameLenght,
            ErrorMessage = "The field {0} is not valid! Must be between of {2} and {1} symbols.")]
        public string? Name { get; set; }

        [Required]
        ICollection<TeamPostModel> Teams { get; set; }
    }
}
