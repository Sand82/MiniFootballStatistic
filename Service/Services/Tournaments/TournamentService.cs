using Microsoft.EntityFrameworkCore;
using MiniFootballStatistic.Data;
using MiniFootballStatistic.Data.Models;

using MiniFootballStatistic.Models.Schema;
using MiniFootballStatistic.Models.Tournament.TournamentPost;

using static MiniFootballStatistic.GlobalConstants.Constans;

namespace MiniFootballStatistic.Services.Tournaments
{
    public class TournamentService : ITournamentService
    {
        private readonly FoodballStatisticDbContext data;

        public TournamentService(FoodballStatisticDbContext data)
        {
            this.data = data;
        }

        public async Task<bool> CreateTournament(TournamentPostModel model, string userId, DateTime date)
        {
            Tournament tournament = new();

            tournament.UserId = userId;
            tournament.ShcemaLength = model.Teams.Count();
            tournament.Name = model.Name;
            tournament.Levels = await GetLevels(model.Teams.Count());
            tournament.CreatedOn = date;

            tournament.Teams = model.Teams.Select(t => new Team
            {
                Name = t.Name,
                TournamentId = tournament.Id,
                TournamentPosition = t.TournamentPosition,
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

            var teams = CreateEmptyTeamInfoModels(model.TournamentPositions, model.Teams.Count());

            tournament.Teams.AddRange(teams);

            var isAddedInDataBase = await AddToDatabase(tournament);

            return isAddedInDataBase;
        }

        public async Task FinishedTournament(string userId)
        {
            Tournament? tournament = await this.data.Tournaments
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.Id)
                .FirstOrDefaultAsync();

            if (tournament != null)
            {
               await SetTournament(tournament);
            }
        }

        public async Task<List<SchemaViewModel>> GetSchemasAsync()
        {
            var schemas = await this.data.Schemas
                .Select(x => new SchemaViewModel
                {
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    UserId = x.UserId,
                    PositionCount = x.PositionCount,

                }).ToListAsync();

            return schemas;
        }

        public async Task<bool> CheckForFreeTournamentName(string name)
        {
            var isExsist = false;

            var tournament = await data.Tournaments
           .Where(t => t.Name == name && t.isDelete == false)
           .FirstOrDefaultAsync();

            if (tournament != null)
            {
                isExsist = true;
            }

            return isExsist;
        }

        private List<Team> CreateEmptyTeamInfoModels(int shcemaLength, int existingModelsCount)
        {
            var emptyModels = new List<Team>();

            Task.Run(() =>
            {
                for (int i = existingModelsCount; i < (shcemaLength * 2) - 2; i++)
                {
                    var model = new Team { Name = EditTeamName, TournamentPosition = i + 1 };

                    emptyModels.Add(model);
                }

            }).GetAwaiter().GetResult();

            return emptyModels;
        }

        private async Task<bool> AddToDatabase(Tournament tournament)
        {
            var isAddedInDatabase = false;

            try
            {
                await this.data.Tournaments.AddAsync(tournament);

                await this.data.SaveChangesAsync();

            }
            catch (Exception)
            {

                return isAddedInDatabase;
            }

            isAddedInDatabase = true;

            return isAddedInDatabase;
        }

        private async Task SetTournament(Tournament tournament)
        {
            tournament.isAddedInDatabase = true;

            await this.data.SaveChangesAsync();
        }

        private Task<int> GetLevels(int positionCount)
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

            return Task.FromResult(count);
        }       
    }
}
