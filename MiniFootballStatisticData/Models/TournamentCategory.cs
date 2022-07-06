using System.ComponentModel.DataAnnotations;

using static MiniFootballStatisticData.Data.DataConstants;

namespace MiniFootballStatisticData.Data.Models
{
    public class TournamentCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(MaxTournamentTypeNameLength)]
        public string? Name { get; set; }

        [Required]
        [StringLength(MaxImageUrlLength)]
        public string? ImageUrl { get; set; }

        [Required]
        [StringLength(MaxImageUrlLength)]
        public string? Descrioption { get; set; }
    }
}
