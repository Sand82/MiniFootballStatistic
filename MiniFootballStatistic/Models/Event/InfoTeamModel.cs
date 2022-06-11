namespace MiniFootballStatistic.Models.Event
{
    public class InfoTeamModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? PositionResult { get; set; }

        public int TournamentPosition { get; set; }

        public int? ScoredGoals { get; set; }
        
        public int? AccumolateGoals { get; set; }

        public int? Difference { get; set; }
    }
}
