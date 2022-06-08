using MiniFootballStatistic.Data.Models;
using MiniFootballStatistic.Models.Tournament;

namespace MiniFootballStatistic.Services.Events
{
    public interface IEventService
    {
        public ICollection<TournamentViewModel> GetTournaments();

        public Tournament GetTournamentById(int id);

        public void DeleteTournament(Tournament tournament);
    }
}
