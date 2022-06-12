using MiniFootballStatistic.Models.Player;

namespace MiniFootballStatistic.Services.Players
{
    public interface IPlayerService
    {
        public PlayerTeamEditModel FindPlayers(int tournamentId, int teamId, string teamName);
    }
}
