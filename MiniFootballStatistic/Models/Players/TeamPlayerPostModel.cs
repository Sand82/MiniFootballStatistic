using MiniFootballStatistic.Models.Player;

namespace MiniFootballStatistic.Models.Players
{
    public class TeamPlayerPostModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<PlayerPostModel>? Players { get; set; }
    }
}
