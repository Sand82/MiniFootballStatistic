using System.ComponentModel.DataAnnotations;

using static MiniFootballStatistic.Data.DataConstants;

namespace MiniFootballStatistic.Data.Models
{
    public class Schema
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(MaxSchemaNameLenght)]
        public string? Name { get; set; }
        
        public string? UserId { get; set; }

        [Required]
        [StringLength(MaxImageUrlLength)]
        public string? ImageUrl { get; set; }

        public int PositionCount { get; set; }

    }
}
