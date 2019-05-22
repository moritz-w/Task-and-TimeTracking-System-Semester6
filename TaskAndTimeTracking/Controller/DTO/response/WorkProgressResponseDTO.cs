using System;

namespace TaskAndTimeTracking.Controller.DTO.response
{
    public class WorkProgressResponseDTO
    {
        
        public DateTime Start { get; set; }
        
        public DateTime End { get; set; }
        
        public string Description { get; set; }

        public virtual UserResponseDTO Person { get; set; } 
    }
}