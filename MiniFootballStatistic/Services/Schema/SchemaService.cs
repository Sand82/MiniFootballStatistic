using MiniFootballStatistic.Data;
using MiniFootballStatistic.Models.Schema;

namespace MiniFootballStatistic.Services.Schema
{
    public class SchemaService  : ISchemaService
    {
        private readonly FoodballStatisticDbContext data;

        public SchemaService(FoodballStatisticDbContext data)
        {
            this.data = data;
        }

        public List<SchemViewModel> GetSchemas()
        {
            List<SchemViewModel> schemas = null;

            Task.Run(() => 
            { 
                schemas = this.data.Schemas.Select(x => new SchemViewModel 
                { 
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    UserId = x.UserId,
                    PositionCount = x.PositionCount,

                }).ToList();

            }).GetAwaiter().GetResult();

            return schemas;
        }
    }
}
