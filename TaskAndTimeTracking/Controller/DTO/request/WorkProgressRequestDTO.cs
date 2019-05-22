using System;
using System.ComponentModel.DataAnnotations;

namespace TaskAndTimeTracking.Controller.DTO.request
{
    public class WorkProgressRequestDTO : BaseDTO
    {
        
        [Required]
        public DateTime Start { get; set; }
        
        [Required]
        public DateTime End { get; set; }
        
        [MinLength(2), MaxLength(100)]
        public string Description { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int Person { get; set; } 
    }
}