using MiniFootballStatistic.Models.Schema;

namespace MiniFootballStatistic.Services.Schema
{
    public interface ISchemaService
    {
        public List<SchemViewModel> GetSchemas();
    }
}
