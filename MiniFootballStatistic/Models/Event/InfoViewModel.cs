namespace MiniFootballStatistic.Models.Event
{
    public class InfoViewModel
    {
        public int Id { get; set; }

        public string TournamentName { get; set; }
        
        public int ShcemaLength { get; set; }

        public int Levels { get; set; }

        public List<InfoTeamModel> Teams { get; set; }
    }
}
