using MiniFootballStatistic.Data.Models;
using MiniFootballStatistic.Models.Event;
using MiniFootballStatistic.Models.Tournament;
using MiniFootballStatistic.Models.Tournament.TurnamentEdit;

namespace MiniFootballStatistic.Services.Events
{
    public interface IEventService
    {
        public ICollection<TournamentViewModel> GetTournaments();

        public Tournament GetTournamentById(int id);

        public InfoViewModel GetInfoViewModel(int id);

        public TournamentEditModel GetTournamentEditModel(int id);

        public void DeleteTournament(Tournament tournament);
    }
}
