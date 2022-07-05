namespace MiniFootballStatistic.Models.Player
{
    public class PlayerTeamEditModel
    {
        public int Id { get; set; }

        public int TournamentId { get; set; }

        public string Name { get; set; }

        public IList<PlayerEditModel> Players { get; set; }       
    }
}
