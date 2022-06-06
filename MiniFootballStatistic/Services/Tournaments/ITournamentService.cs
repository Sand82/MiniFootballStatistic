using MiniFootballStatistic.Models.ThirdPart;
using MiniFootballStatistic.Models.Tournament;

namespace MiniFootballStatistic.Services.Tournaments
{
    public interface ITournamentService
    {
        public List<TournamentViewModel> GetSchemas();

        public void CreateChampionship(TournamentPostModel model, string userId);

        public TournamentCreatePlayersModel GetTeams(string userId);
    }
}
