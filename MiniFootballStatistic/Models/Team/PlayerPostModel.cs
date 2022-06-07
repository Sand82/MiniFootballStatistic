using System.ComponentModel.DataAnnotations;

namespace MiniFootballStatistic.Models.Team
{
    public class PlayerPostModel
    {
        [Required]
        [Display(Name = "Player Name")]        
        public string Name { get; set; }

        public int? TeamId { get; set; }

    }
}
