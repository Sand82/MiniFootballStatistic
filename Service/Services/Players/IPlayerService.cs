using MiniFootballStatistic.Data.Models;
using MiniFootballStatistic.Models.Player;

namespace MiniFootballStatistic.Services.Players
{
    public interface IPlayerService
    {
        public Task<PlayerTeamEditModel> FindPlayers(int tournamentId, int teamId, string teamName);

        public Task SetPlayersStatistic(PlayerTeamEditModel model, Team team);

        public Task<Tournament> GetTournament(int tournamentId);

        public Task<StatisticPlayersModel> GetTopPlayersStatistic(Tournament tournament);
    }
}
