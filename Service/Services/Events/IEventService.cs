using MiniFootballStatistic.Data.Models;
using MiniFootballStatistic.Models.Event;
using MiniFootballStatistic.Models.Tournament;
using MiniFootballStatistic.Models.Tournament.TurnamentEdit;

namespace MiniFootballStatistic.Services.Events
{
    public interface IEventService
    {
        public Task<ICollection<TournamentViewModel>> GetTournaments();

        public Task<Tournament> GetTournamentById(int id);

        public Task<InfoViewModel> GetInfoViewModel(int id);

        public Task<TournamentEditModel> GetTournamentEditModel(int id);

        public Task DeleteTournament(Tournament tournament);
    }
}
