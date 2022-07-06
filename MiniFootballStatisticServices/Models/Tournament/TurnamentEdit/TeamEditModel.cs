using MiniFootballStatisticServices.Models.Tournament.TournamentEdit;

using System.ComponentModel.DataAnnotations;

using static MiniFootballStatisticData.Data.DataConstants;

namespace MiniFootballStatisticServices.Models.Tournament.TurnamentEdit
{
    public class TeamEditModel
    {
        public TeamEditModel()
        {
            this.Players = new List<PlayerEditModel>();
        }

        public int Id { get; set; }

        public int TournamentId { get; set; }

        [Required]
        [Display(Name = "Team Name")]
        [StringLength(MaxNameLenght, MinimumLength = MinNameLenght,
            ErrorMessage = "The field {0} is not valid! Must be between of {2} and {1} symbols.")]
        public string? Name { get; set; }
                
        public int TournamentPosition { get; set; }
                
        [Display(Name = "Scored Goals")]
        public int? PositionResult { get; set; }

        public List<PlayerEditModel>? Players { get; set; }
    }
}
