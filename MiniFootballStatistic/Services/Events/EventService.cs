using MiniFootballStatistic.Data;
using MiniFootballStatistic.Data.Models;
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
                .Where(t => t.isAddedInDatabase == true)
                .Select(t => new TournamentViewModel 
                { 
                    Name = t.Name,
                    CreationData = t.CreatedOn.ToString(),
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


        //private string GetUserEmail(string? userId)
        //{
        //    string email = null;

        //    Task.Run(() =>
        //    {               
        //        email = 

        //    }).GetAwaiter().GetResult();
        //}
    }
}
