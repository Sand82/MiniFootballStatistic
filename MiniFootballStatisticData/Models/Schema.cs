using System.ComponentModel.DataAnnotations;

using static MiniFootballStatisticData.Data.DataConstants;

namespace MiniFootballStatisticData.Data.Models
{
    public class Schema
    {
        public Schema()
        {
            this.Tournaments = new HashSet<Tournament>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(MaxNameLenght)]
        public string? Name { get; set; }
        
        public string? UserId { get; set; }

        [Required]
        [StringLength(MaxImageUrlLength)]
        public string? ImageUrl { get; set; }

        public int PositionCount { get; set; }

        ICollection<Tournament> Tournaments { get; set; }

    }
}
