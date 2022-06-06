using MiniFootballStatistic.Models.Players;

namespace MiniFootballStatistic.Services.Players
{
    public interface IPlayerService
    {
        public TeamPlayerPostModel GetTeamById(int teamId);
    }
}
