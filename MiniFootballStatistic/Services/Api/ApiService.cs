using MiniFootballStatistic.Data;
using MiniFootballStatistic.Data.Models;

using Microsoft.EntityFrameworkCore;

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

        public void AdjustStatistic(Team team, Team opponentTeam, int shemaPosition, int schemaLength)
        {
            Task.Run(() =>
            {

                opponentTeam.AccumulateGoals = team.ScoredGoals;

                team.AccumulateGoals = opponentTeam.ScoredGoals;

                if (opponentTeam.ScoredGoals > team.ScoredGoals)
                {
                    opponentTeam.IsLose = false;
                    team.IsLose = true;
                    SetTeam(opponentTeam, shemaPosition, schemaLength);
                }
                else
                {
                    opponentTeam.IsLose = true;
                    team.IsLose = false;
                    SetTeam(team, shemaPosition, schemaLength);
                }

                this.data.SaveChanges();

            }).GetAwaiter().GetResult();
        }

        private void SetTeam(Team currTeam, int shemaPosition, int schemaLength)
        {
            Task.Run(() =>
            {
                var team = this.data.Team
                    .Include(t => t.Players)
                    .Where(t => t.TournamentId == currTeam.TournamentId && t.TournamentPosition == shemaPosition)
                    .FirstOrDefault();

                if ((schemaLength - 2) < shemaPosition && team == null)
                {
                    currTeam.IsChampion = true;
                }
                else
                {
                    team.Name = currTeam.Name;
                    team.Players = currTeam.Players;
                }

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
