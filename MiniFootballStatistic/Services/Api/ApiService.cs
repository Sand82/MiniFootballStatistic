using MiniFootballStatistic.Data;
using MiniFootballStatistic.Data.Models;

namespace MiniFootballStatistic.Services.Api
{
    public class ApiService : IApiService
    {
        private readonly FoodballStatisticDbContext data;

        public ApiService(FoodballStatisticDbContext data)
        {
            this.data = data;
        }        

        public Team FindTeam(int tournamentId, int teamId)
        {
            Team? team = null;               

            Task.Run(() =>
            {
                team = this.data.Team
                .Where(x => x.TournamentId == tournamentId && x.Id == teamId)
                .FirstOrDefault();

            }).GetAwaiter().GetResult();

            return team;
        }

        public void AdjustStatistic(Team previusTeam, Team team)
        {            
            Task.Run(() =>
            {
                previusTeam.AccumolateGoals = team.ScoredGoals;

                team.AccumolateGoals = previusTeam.ScoredGoals;

                if (previusTeam.ScoredGoals > team.ScoredGoals)
                {
                    previusTeam.IsLose = false;
                    team.IsLose = true;
                }
                else
                {
                    previusTeam.IsLose = true;
                    team.IsLose = false;
                }

                this.data.SaveChanges();

            }).GetAwaiter().GetResult();

        }

        public void SetName(Team team, string name)
        {
            Task.Run(() =>
            {
                team.Name = name;
                
                this.data.SaveChanges();

            }).GetAwaiter().GetResult();
        }

        public void SetStatistic(Team team, int goals)
        {
            Task.Run(() =>
            {
                team.ScoredGoals = goals;

                team.PositionResult = goals;

                this.data.SaveChanges();

            }).GetAwaiter().GetResult();
        }
    }
}
