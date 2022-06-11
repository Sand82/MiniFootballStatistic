using MiniFootballStatistic.Data;
using MiniFootballStatistic.Data.Models;
using MiniFootballStatistic.Models.Schema;
using MiniFootballStatistic.Models.Tournament.TournamentPost;

namespace MiniFootballStatistic.Services.Tournaments
{
    public class TournamentService : ITournamentService
    {
        private readonly FoodballStatisticDbContext data;

        public TournamentService(FoodballStatisticDbContext data)
        {
            this.data = data;
        }

        public bool CreateTournament(TournamentPostModel model, string userId, DateTime date)
        {
            Tournament tournament = new();           

            Task.Run(() =>
            {
                tournament.UserId = userId;
                tournament.ShcemaLength = model.Teams.Count();
                tournament.Name = model.Name;
                tournament.Levels = GetLevels(model.Teams.Count());
                tournament.CreatedOn = date;

                tournament.Teams = model.Teams.Select(t => new Team
                {
                    Name = t.Name,
                    TournamentId = tournament.Id,
                    TournamentPosition = t.TournamentPosition,
                    ScoredGoals = 0,
                    AccumulateGoals = 0,                                    
                    Players = t.Players.Select(p => new Player
                    {
                        Name = p.Name,
                        Assists = 0,
                        Goals = 0,
                        MachesCount = 0,                        
                    })
                    .ToList()
                })
                .ToList();

            }).GetAwaiter().GetResult();

            var teams = CreateEmptyTeamInfoModels(model.TournamentPositions, model.Teams.Count());

            tournament.Teams.AddRange(teams);

            var isAddedInDataBase = AddToDatabase(tournament);

            return isAddedInDataBase;
        }
        
        public void FinishedTournament(string userId)
        {
            Tournament? tournament = null;

            Task.Run(() =>
            {
                tournament = this.data.Tournaments
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.Id)
                .FirstOrDefault();               

            }).GetAwaiter().GetResult();

            if (tournament != null)
            {
                SetTournament(tournament);
            }
        }
        
        public List<SchemaViewModel> GetSchemas()
        {
            List<SchemaViewModel>? schemas = null;

            Task.Run(() => 
            { 
                schemas = this.data.Schemas.Select(x => new SchemaViewModel
                { 
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    UserId = x.UserId,
                    PositionCount = x.PositionCount,

                }).ToList();

            }).GetAwaiter().GetResult();

            return schemas;
        }

        private List<Team> CreateEmptyTeamInfoModels(int shcemaLength, int existingModelsCount)
        {
            var emptyModels = new List<Team>();

            for (int i = existingModelsCount; i < (shcemaLength * 2) - 2; i++)
            {
                var model = new Team { Name = "Not played yet", PositionResult = 0, TournamentPosition = i + 1 };

                emptyModels.Add(model);
            }

            return emptyModels;
        }


        private bool AddToDatabase(Tournament tournament)
        {
            var isAddedInDatabase = false;

            Task.Run(() =>
            {
                this.data.Tournaments.Add(tournament);

                this.data.SaveChanges();

            }).GetAwaiter().GetResult();

            return isAddedInDatabase = true;
        }

        private void SetTournament(Tournament tournament)
        {
            Task.Run(() =>
            {
                tournament.isAddedInDatabase = true;

                this.data.SaveChanges();

            }).GetAwaiter().GetResult();
        }

        private int GetLevels(int positionCount)
        {
            var count = 0;

            int number = positionCount;

            for (int i = 0; i < positionCount; i++)
            {
                number = number / 2;                

                if (number == 0)
                {
                    break;
                }

                count++;
            }

            return count;
        }
    }
}
