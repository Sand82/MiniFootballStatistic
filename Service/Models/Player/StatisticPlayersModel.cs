namespace MiniFootballStatistic.Models.Player
{
    public class StatisticPlayersModel
    {
        public Dictionary<string, int> PlayesGoals { get; set; } = new Dictionary<string, int>();

        public Dictionary<string, int> PlayesAssists { get; set; } = new Dictionary<string, int>();
    }
}
