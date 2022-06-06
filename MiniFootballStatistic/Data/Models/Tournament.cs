using System.ComponentModel.DataAnnotations;

using static MiniFootballStatistic.Data.DataConstants;

namespace MiniFootballStatistic.Data.Models
{
    public class Tournament
    {
        public Tournament()
        {
            this.Teams = new HashSet<Team>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(MaxTournamentTypeNameLength)]
        public string? Name { get; set; }

        [Required]
        public string? UserId { get; set; }

        [Range(MinSchemaTypeLenght,MaxSchemaTypeLenght)]
        public int ShcemaLength { get; set; }

        public int SchemId { get; set; }

        public Schema Schemas { get; set; }

        public ICollection<Team> Teams { get; set; }
    }
}
