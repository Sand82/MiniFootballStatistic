using MiniFootballStatistic.Data;
using MiniFootballStatistic.Models.Player;

using Microsoft.EntityFrameworkCore;
using MiniFootballStatistic.Data.Models;

namespace MiniFootballStatistic.Services.Players
{
    public class PlayerService : IPlayerService
    {
        private readonly FoodballStatisticDbContext data;

        public PlayerService(FoodballStatisticDbContext data)
        {
            this.data = data;
        }

        public PlayerTeamEditModel FindPlayers(int tournamentId, int teamId, string teamName)
        {
            PlayerTeamEditModel? players = null;

            Task.Run(() =>
            {
                players = this.data.Team
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
                .FirstOrDefault();

            }).GetAwaiter().GetResult();

            return players;
        }

        public void SetPlayersStatistic(PlayerTeamEditModel model, Team team)
        {
            Task.Run(() =>
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

                this.data.SaveChanges();

            }).GetAwaiter().GetResult();
        }       
    }
}
