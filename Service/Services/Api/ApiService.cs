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

        public async Task<Team> FindTeam(int tournamentId, int teamId)
        {
            Team? team = await this.data.Team
                 .Include(t => t.Players)
                 .Where(x => x.TournamentId == tournamentId && x.Id == teamId)
                 .FirstOrDefaultAsync();

            return team;
        }

        public async Task AdjustStatistic(Team team, Team opponentTeam, int shemaPosition, int schemaLength)
        {

            opponentTeam.AccumulateGoals = team.ScoredGoals;

            team.AccumulateGoals = opponentTeam.ScoredGoals;

            if (opponentTeam.ScoredGoals > team.ScoredGoals)
            {
                opponentTeam.IsLose = false;
                team.IsLose = true;
                await SetTeam(opponentTeam, shemaPosition, schemaLength);
            }
            else
            {
                opponentTeam.IsLose = true;
                team.IsLose = false;
                await SetTeam(team, shemaPosition, schemaLength);
            }

            await this.data.SaveChangesAsync();

        }

        private async Task SetTeam(Team currTeam, int shemaPosition, int schemaLength)
        {

            var team = await this.data.Team
            .Include(t => t.Players)
            .Where(t => t.TournamentId == currTeam.TournamentId && t.TournamentPosition == shemaPosition)
            .FirstOrDefaultAsync();

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
        }

        public Task<List<Player>> GetTeams(Team team)
        {
            var players = team.Players
                .Select(p => new Player
                {
                    Name = p.Name,
                    Goals = p.Goals,
                    Assists = p.Assists,
                })
                .ToList();

            return Task.FromResult(players);
        }

        public async Task SetName(Team team, string name)
        {
            team.Name = name;

            await this.data.SaveChangesAsync();
        }

        public async Task SetStatistic(Team team, int goals)
        {
            team.ScoredGoals = goals;

            team.PositionResult = goals;

            await this.data.SaveChangesAsync();
        }
    }
}
