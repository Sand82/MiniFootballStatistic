using MiniFootballStatistic.Data;
using MiniFootballStatistic.Models.Player;

using Microsoft.EntityFrameworkCore;

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
    }
}
