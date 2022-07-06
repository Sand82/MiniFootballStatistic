using MiniFootballStatisticData.Data;
using MiniFootballStatisticServices.Models.Player;

using Microsoft.EntityFrameworkCore;
using MiniFootballStatisticData.Data.Models;
using MiniFootballStatisticData.Data.Models;

namespace MiniFootballStatisticServices.Services.Players
{
    public class PlayerService : IPlayerService
    {
        private readonly FoodballStatisticDbContext data;

        public PlayerService(FoodballStatisticDbContext data)
        {
            this.data = data;
        }

        public async Task<PlayerTeamEditModel> FindPlayers(int tournamentId, int teamId, string teamName)
        {
           var players = await this.data.Team
            .Include(t => t.Players)
            .Where(t => t.TournamentId == tournamentId && t.Id == teamId)
            .Select(t => new PlayerTeamEditModel
            {
                Id = t.Id,
                Name = t.Name,
                TournamentId = tournamentId,
                Players = t.Players.Select(p => new PlayerEditModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    TeamId = p.TeamId,
                    Goals = p.Goals,
                    Assists = p.Assists,
                })
                .ToList()
            })
            .FirstOrDefaultAsync();

            return players;
        }

        public Task<StatisticPlayersModel> GetTopPlayersStatistic(Tournament tournament)
        {
            StatisticPlayersModel model = new();

            foreach (var team in tournament.Teams)
            {
                foreach (var player in team.Players)
                {
                    if (!model.PlayesAssists.ContainsKey(player.Name))
                    {
                        model.PlayesAssists.Add(player.Name, 0);
                    }

                    model.PlayesAssists[player.Name] += player.Assists;

                    if (!model.PlayesGoals.ContainsKey(player.Name))
                    {
                        model.PlayesGoals.Add(player.Name, 0);
                    }

                    model.PlayesGoals[player.Name] += player.Goals;
                }
            }            

            return Task.FromResult(model);
        }

        public async Task<Tournament> GetTournament(int tournamentId)
        {
            var tournamnet = await this.data.Tournaments
              .Include(t => t.Teams).ThenInclude(t => t.Players)
              .Where(t => t.Id == tournamentId)
              .FirstOrDefaultAsync();

            return tournamnet;
        }

        public async Task SetPlayersStatistic(PlayerTeamEditModel model, Team team)
        {
        
            for (int i = 0; i < team.Players.Count; i++)
            {
                if (model.Players[i].Id == team.Players[i].Id)
                {
                    team.Players[i].Name = model.Players[i].Name;
                    team.Players[i].Goals = model.Players[i].Goals;
                    team.Players[i].Assists = model.Players[i].Assists;
                }
            }

            await  this.data.SaveChangesAsync();                       
        }
    }
}
