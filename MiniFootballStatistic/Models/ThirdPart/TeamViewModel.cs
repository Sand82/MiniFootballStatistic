namespace MiniFootballStatistic.Models.ThirdPart
{
    public class TeamViewModel
    {
        public string Name { get; set; }

        public IList<PlayerPostModel> Players { get; set; }
    }
}
