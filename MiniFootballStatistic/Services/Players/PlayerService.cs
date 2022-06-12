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

        public StatisticPlayersModel GetTopPlayersStatistic(Tournament tournament)
        {
            StatisticPlayersModel model = new();

            //Task.Run(() =>
            //{
                foreach (var team in tournament.Teams)
                {
                    foreach (var player in team.Players)
                    {
                        if (!model.PlayesGoals.ContainsKey(player.Name))
                        {
                            model.PlayesGoals.Add(player.Name, player.Goals);
                        }

                        model.PlayesGoals[player.Name] += player.Goals;

                        if (!model.PlayesAssists.ContainsKey(player.Name))
                        {
                            model.PlayesAssists.Add(player.Name, player.Assists);
                        }

                        model.PlayesAssists[player.Name] += player.Assists;
                    }
                }

            //}).GetAwaiter().GetResult();

            return model;
        }

        public Tournament GetTournament(int tournamentId)
        {
            Tournament? tournamnet = null;

            Task.Run(() =>
            {
                tournamnet = this.data.Tournaments
                .Include(t => t.Teams).ThenInclude(t => t.Players)
                .Where(t => t.Id == tournamentId)
                .FirstOrDefault();

            }).GetAwaiter().GetResult();

            return tournamnet;
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
