namespace MiniFootballStatistic.Models.ThirdPart
{
    public class TournamentCreatePlayersModel
    {
        public string Name { get; set; }

        public IList<TeamViewModel>? Teams { get; set; }

    }
}
