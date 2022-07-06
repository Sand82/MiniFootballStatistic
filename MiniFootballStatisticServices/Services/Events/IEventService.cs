using MiniFootballStatisticData.Data.Models;
using MiniFootballStatisticServices.Models.Event;
using MiniFootballStatisticServices.Models.Tournament;
using MiniFootballStatisticServices.Models.Tournament.TurnamentEdit;

namespace MiniFootballStatisticServices.Services.Events
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
