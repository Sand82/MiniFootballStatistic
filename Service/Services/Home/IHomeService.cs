using MiniFootballStatistic.Models.Home;

namespace MiniFootballStatistic.Services.Home
{
    public interface IHomeService
    {
        public Task<IEnumerable<IndexViewModel>> GetTournaments();
    }
}
