using System.ComponentModel.DataAnnotations;

using static MiniFootballStatistic.Data.DataConstants;

namespace MiniFootballStatistic.Data.Models
{
    public class TournamentCategory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(MaxTournamentTypeNameLength)]
        public string? Name { get; set; }

        [Required]
        [StringLength(MaxPictureUrlLength)]
        public string? ImageUrl { get; set; }

        [Required]
        [StringLength(MaxPictureUrlLength)]
        public string? Descrioption { get; set; }
    }
}
