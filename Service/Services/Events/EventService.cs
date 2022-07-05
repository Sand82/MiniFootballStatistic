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

        public async Task<Tournament> GetTournamentById(int id)
        {
            var tournament = await this.data
            .Tournaments
            .FirstOrDefaultAsync(x => x.Id == id);

            return tournament;
        }

        public async Task<ICollection<TournamentViewModel>> GetTournaments()
        {
            var tournaments = await this.data.Tournaments
                 .Where(t => t.isAddedInDatabase == true && t.isDelete == false)
                 .Select(t => new TournamentViewModel
                 {
                     Name = t.Name,
                     CreationData = t.CreatedOn.Value.Date.ToString(),
                     Id = t.Id,
                     SchemaLenght = t.ShcemaLength,
                     UserId = t.UserId,
                     WinnerTeam = t.Teams.Where(t => t.IsChampion == true).Select(t => t.Name).FirstOrDefault(),
                 })
                .ToListAsync();

            return tournaments;
        }

        public Task DeleteTournament(Tournament tournament)
        {
            tournament.isDelete = true;

            this.data.SaveChangesAsync();

            return Task.CompletedTask;
        }

        public async Task<InfoViewModel> GetInfoViewModel(int id)
        {
            InfoViewModel? model = await this.data.Tournaments
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
                .FirstOrDefaultAsync();

            return model;
        }

        public async Task<TournamentEditModel> GetTournamentEditModel(int id)
        {
            TournamentEditModel? model = await this.data.Tournaments
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
                .FirstOrDefaultAsync();

            return model;
        }
    }
}
