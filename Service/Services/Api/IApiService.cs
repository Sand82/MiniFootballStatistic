using MiniFootballStatistic.Data.Models;

namespace MiniFootballStatistic.Services.Api
{
    public interface IApiService
    {
        public Task<Team> FindTeam(int tournamentId, int teamId);

        public Task SetName(Team team, string name);

        public Task SetStatistic(Team team, int goals);

        public Task AdjustStatistic(Team previusTeam,Team team,int shemaPosition, int schemaLength);

        public Task<List<Player>> GetTeams(Team team);
    }
}
