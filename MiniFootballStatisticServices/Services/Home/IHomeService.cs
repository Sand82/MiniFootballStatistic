using MiniFootballStatisticServices.Models.Home;

namespace MiniFootballStatisticServices.Services.Home
{
    public interface IHomeService
    {
        public Task<IEnumerable<IndexViewModel>> GetTournaments();
    }
}
