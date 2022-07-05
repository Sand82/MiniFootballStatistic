using MiniFootballStatistic.Models.Schema;
using MiniFootballStatistic.Models.Tournament.TournamentPost;

namespace MiniFootballStatistic.Services.Tournaments
{
    public interface ITournamentService
    {
        public Task<List<SchemaViewModel>> GetSchemasAsync();

        public Task<bool> CreateTournament(TournamentPostModel model, string userId, DateTime date);

        public Task FinishedTournament(string userId);

        public Task<bool> CheckForFreeTournamentName(string name);
    }
}
