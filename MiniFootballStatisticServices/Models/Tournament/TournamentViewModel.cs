using System.ComponentModel.DataAnnotations;

namespace MiniFootballStatisticServices.Models.Tournament
{
    public class TournamentViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CreationData { get; set; }

        public int SchemaLenght { get; set; }

        public string UserId { get; set; }

        public string? WinnerTeam { get; set; }
    }
}
