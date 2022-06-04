using MiniFootballStatistic.Models.Home;

namespace MiniFootballStatistic.Services.Home
{
    public interface IHomeService
    {
        public IEnumerable<IndexViewModel> GetTournaments();
    }
}
