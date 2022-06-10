using System.ComponentModel.DataAnnotations;

namespace MiniFootballStatistic.Models.Tournament.TurnamentEdit
{
    public class TournamentEditModel
    {
        public TournamentEditModel()
        {
            this.Teams = new List<TeamEditModel>();
        }

        public int Id { get; set; }

        public string UserId { get; set; }

        public string? Name { get; set; }

        public int Levels { get; set; }

        public int SchemaLenght { get; set; }

        [Required]
        public List<TeamEditModel>? Teams { get; set; }
    }
}
