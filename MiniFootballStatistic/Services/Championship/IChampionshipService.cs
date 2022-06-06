using MiniFootballStatistic.Models.Championship;

namespace MiniFootballStatistic.Services.Championship
{
    public interface IChampionshipService
    {
        public List<ChampionshipViewModel> GetSchemas();

        public void CreateChampionship(ChampionshipPostModel model, string userId);

       
    }
}
