using MiniFootballStatistic.Data;
using MiniFootballStatistic.Data.Models;

using static MiniFootballStatistic.GlobalConstants.Constans;

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

        public void AdjustStatistic(Team previusTeam, Team team)
        {
            Task.Run(() =>
            {

                previusTeam.AccumulateGoals = team.ScoredGoals;

                team.AccumulateGoals = previusTeam.ScoredGoals;

                if (previusTeam.ScoredGoals > team.ScoredGoals)
                {
                    previusTeam.IsLose = false;
                    team.IsLose = true;
                    SetTeam(previusTeam);
                }
                else
                {
                    previusTeam.IsLose = true;
                    team.IsLose = false;
                    SetTeam(team);
                }

                this.data.SaveChanges();

            }).GetAwaiter().GetResult();
        }

        private void SetTeam(Team currTeam)
        {
            Task.Run(() =>
            {
                bool IsemptyTeamExist = false;

                var tournament = this.data.Tournaments
                    .Include(t => t.Teams)
                    .ThenInclude(t => t.Players)
                    .Where(t => t.Id == currTeam.TournamentId)
                    .FirstOrDefault();

                foreach (var team in tournament.Teams.OrderBy(t => t.Id))
                {
                    if (team.Name == EditTeamName)
                    {
                        team.Name = currTeam.Name;
                        team.Players = currTeam.Players;
                        IsemptyTeamExist = true;
                        break;
                    }
                }

                if (!IsemptyTeamExist)
                {
                    currTeam.IsChampion = true;
                }

            }).GetAwaiter().GetResult();

            //this.data.SaveChanges();
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
