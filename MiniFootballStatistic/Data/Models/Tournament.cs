using System.ComponentModel.DataAnnotations;

using static MiniFootballStatistic.Data.DataConstants;

namespace MiniFootballStatistic.Data.Models
{
    public class Tournament
    {
        public Tournament()
        {
            this.Teams = new List<Team>();
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

        public int Levels { get; set; }

        public DateTime? CreatedOn { get; set; }

        public bool isAddedInDatabase { get; set; } = false;

        public bool isDelete { get; set; } = false;

        public List<Team> Teams { get; set; }
    }
}
