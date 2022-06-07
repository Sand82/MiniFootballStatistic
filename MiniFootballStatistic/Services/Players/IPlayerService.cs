using MiniFootballStatistic.Models.Players;

namespace MiniFootballStatistic.Services.Players
{
    public interface IPlayerService
    {
        public TeamPlayerPostModel GetTeamByTeamId(string userId);

        public void CreatePlayers(TeamPlayerPostModel model, int modelId);
    }
}
