using MiniFootballStatistic.Models.Tournament;
using MiniFootballStatistic.Models.Tournament.TournamentPost;

namespace MiniFootballStatistic.Services.Tournaments
{
    public interface ITournamentService
    {
        public List<TournamentViewModel> GetSchemas();

        public bool CreateChampionship(TournamentPostModel model, string userId);

        public void FinishedTournament(string userId);
    }
}
