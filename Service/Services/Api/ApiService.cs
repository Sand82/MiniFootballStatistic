using Microsoft.EntityFrameworkCore;
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
                .Include(t => t.Players)
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

                bool isTeamHavePlayers = true;

                if ((schemaLength - 2) < shemaPosition && team == null)
                {
                    var previousChampion = this.data.Team.Where(t => t.IsChampion == true).FirstOrDefault();

                    if (previousChampion != null)
                    {
                        previousChampion.IsChampion = false;
                    }

                    currTeam.IsChampion = true;
                }
                else
                {
                    if (team.Players.Count() == 0)
                    {
                        isTeamHavePlayers = false;
                    }

                    team.Name = currTeam.Name;

                    for (int i = 0; i < currTeam.Players.Count; i++)
                    {
                        if (isTeamHavePlayers)
                        {
                            team.Players[i].Name = currTeam.Players[i].Name;
                        }
                        else
                        {
                            team.Players.Add(new Player
                            {
                                Name = currTeam.Players[i].Name,
                                TeamId = currTeam.Players[i].TeamId,
                            });
                        }

                        team.Players[i].Assists = 0;
                        team.Players[i].Goals = 0;
                        team.Players[i].MachesCount = 0;
                    }
                }

            }).GetAwaiter().GetResult();
        }

        public List<Player> GetTeams(Team team)
        {
            List<Player> players = null;

            Task.Run(() =>
            {
                players = team.Players.Select(p => new Player 
                {
                    Name = p.Name,
                    Goals = p.Goals,
                    Assists = p.Assists,
                })
                .ToList();

            }).GetAwaiter().GetResult();

            return players;
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
