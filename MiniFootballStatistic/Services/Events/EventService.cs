using MiniFootballStatistic.Data;
using MiniFootballStatistic.Data.Models;
using MiniFootballStatistic.Models.Event;
using MiniFootballStatistic.Models.Tournament;
using MiniFootballStatistic.Models.Tournament.TournamentEdit;
using MiniFootballStatistic.Models.Tournament.TurnamentEdit;

using Microsoft.EntityFrameworkCore;

namespace MiniFootballStatistic.Services.Events
{
    public class EventService : IEventService
    {
        private readonly FoodballStatisticDbContext data;

        public EventService(FoodballStatisticDbContext data)
        {
            this.data = data;
        }
        
        public Tournament GetTournamentById(int id)
        {
            Tournament? tournament = null;

            Task.Run(() =>
            { 
               tournament = this.data
               .Tournaments
               .FirstOrDefault(x => x.Id == id);

            }).GetAwaiter().GetResult();

            return tournament;
        }

        public ICollection<TournamentViewModel> GetTournaments()
        {
            List<TournamentViewModel>? tournaments = null;

            Task.Run(() =>
            {
               tournaments = this.data.Tournaments
                .Where(t => t.isAddedInDatabase == true && t.isDelete == false)
                .Select(t => new TournamentViewModel 
                { 
                    Name = t.Name,
                    CreationData = t.CreatedOn.Value.Date.ToString(),
                    Id = t.Id,
                    SchemaLenght = t.ShcemaLength,
                    UserId = t.UserId,
                    //UserEmail = GetUserEmail(t.UserId),
                })
                .ToList();

            }).GetAwaiter().GetResult();             

            return tournaments;
        }

        public void DeleteTournament(Tournament tournament)
        {
            Task.Run(() =>
            {
                tournament.isDelete = true;

                this.data.SaveChanges();

            }).GetAwaiter().GetResult();
        }

        public InfoViewModel GetInfoViewModel(int id)
        {
            InfoViewModel? model = null;

            Task.Run(() =>
            {
                model = this.data.Tournaments
                .Include(t => t.Teams)
                .ThenInclude(t => t.Players)
                .Where(t => t.Id == id)
                .Select(t => new InfoViewModel
                {
                    Id = t.Id,
                    Levels = t.Levels,
                    TournamentName = t.Name,
                    ShcemaLength = t.ShcemaLength,
                    Teams = t.Teams
                    .OrderBy(t => t.Id)
                    .Select(te => new InfoTeamModel
                    {
                        Name = te.Name,
                        PositionResult = te.PositionResult,
                        TournamentPosition = te.TournamentPosition,
                        AccumolateGoals = t.Teams.Where(x => x.Name == te.Name).Sum(x => x.AccumulateGoals),
                        ScoredGoals = t.Teams.Where(x => x.Name == te.Name).Sum(x => x.ScoredGoals),
                        Id = te.Id,                                   
                    })
                    .ToList()
                }) 
                .FirstOrDefault();

            }).GetAwaiter().GetResult();

            return model;
        }

        public TournamentEditModel GetTournamentEditModel(int id)
        {
            TournamentEditModel? model = null;

            Task.Run(() =>
            {
                model = this.data.Tournaments                
                .Where(t => t.Id == id)
                .Include(t => t.Teams)
                .ThenInclude(t => t.Players)                
                .Select(t => new TournamentEditModel
                {
                    Id = t.Id,
                    UserId = t.UserId,
                    Name = t.Name,
                    SchemaLength = t.ShcemaLength,
                    Levels = t.Levels,
                    Teams = t.Teams
                    .OrderBy(te => te.TournamentPosition)
                    .Select(te => new TeamEditModel
                    {
                        Id = te.Id,
                        TournamentId = te.TournamentId,
                        Name = te.Name,
                        PositionResult = te.PositionResult,
                        TournamentPosition = te.TournamentPosition,
                        Players = te.Players.Select(p => new PlayerEditModel
                        {
                           TeamId = p.TeamId,
                           Name = p.Name,
                           Goals = p.Goals,
                           Assists = p.Assists,
                        })
                        .ToList(),
                    })
                    .ToList(),
                })                   
                .FirstOrDefault();

            }).GetAwaiter().GetResult();

            return model;
        }
    }
}
