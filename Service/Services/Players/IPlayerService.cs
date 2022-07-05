using MiniFootballStatistic.Data.Models;
using MiniFootballStatistic.Models.Player;

namespace MiniFootballStatistic.Services.Players
{
    public interface IPlayerService
    {
        public PlayerTeamEditModel FindPlayers(int tournamentId, int teamId, string teamName);

        public void SetPlayersStatistic(PlayerTeamEditModel model, Team team);

        public Tournament GetTournament(int tournamentId);

        public StatisticPlayersModel GetTopPlayersStatistic(Tournament tournament);
    }
}
