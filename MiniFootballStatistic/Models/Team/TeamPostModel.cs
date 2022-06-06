﻿using System.ComponentModel.DataAnnotations;

using static MiniFootballStatistic.Data.DataConstants;

namespace MiniFootballStatistic.Models.Team
{
    public class TeamPostModel
    {
        [Required]        
        [StringLength(MaxNameLenght, MinimumLength = MinNameLenght,
            ErrorMessage = "The field {0} is not valid! Must be between of {2} and {1} symbols.")]        
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Tournament Position")]
        [StringLength(MaxTeamPositionLength, MinimumLength = MinTeamPositionLength,
            ErrorMessage = "The field {0} is not valid! Must be between of {2} and {1} symbols.")]
        public string? TournamentPosition { get; set; }
    }
}
