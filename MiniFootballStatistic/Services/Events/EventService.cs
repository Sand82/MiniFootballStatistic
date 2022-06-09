using MiniFootballStatistic.Data;
using MiniFootballStatistic.Data.Models;
using MiniFootballStatistic.Models.Event;
using MiniFootballStatistic.Models.Tournament;

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

        public InfoViewModel GetTournamentModelById(int id)
        {
            InfoViewModel? model = null;

            Task.Run(() =>
            {
                model = this.data.Tournaments
                .Where(t => t.Id == id)
                .Select(t => new InfoViewModel
                {
                    Id = t.Id,
                    Levels = t.Levels,
                    TournamentName = t.Name,
                    ShcemaLength = t.ShcemaLength,
                    Teams = t.Teams
                    .OrderBy(t => t.TournamentPosition)
                    .Select(te => new InfoTeamModel
                    {
                        Name = te.Name,
                        PositionResult = te.PositionResult,
                        TournamentPosition = te.TournamentPosition,
                        Id = te.Id

                    })                    
                    .ToList()
                })
                .FirstOrDefault();

            }).GetAwaiter().GetResult();

            return model;
        }        
    }
}
