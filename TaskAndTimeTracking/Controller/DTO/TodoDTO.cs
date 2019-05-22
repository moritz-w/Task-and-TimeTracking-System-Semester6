using System;
using System.ComponentModel.DataAnnotations;

namespace TaskAndTimeTracking.Controller.DTO
{
    public class TodoDTO : BaseDTO
    {
        [Required, MinLength(2), MaxLength(100)]
        public String Name { get; set; }
        
        [MinLength(5), MaxLength(500)]
        public String TaskDescription { get; set; }
        
        public bool Done { get; set; }
    }
}