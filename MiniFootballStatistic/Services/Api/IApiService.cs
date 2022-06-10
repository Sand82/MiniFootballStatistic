using MiniFootballStatistic.Data.Models;

namespace MiniFootballStatistic.Services.Api
{
    public interface IApiService
    {
        public Team FindTeam(int tournamentId, int teamId);

        public void SetName(Team team, string name);

        public void SetStatistic(Team team, int goals);

        public void AdjustStatistic(Team previusTeam,Team team);
    }
}
