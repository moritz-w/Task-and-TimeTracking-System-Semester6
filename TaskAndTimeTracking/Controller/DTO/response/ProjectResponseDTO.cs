using System;

namespace TaskAndTimeTracking.Controller.DTO.response
{
    public class ProjectResponseDTO : BaseDTO
    {
        public String Name { get; set; }
        
        public String Description { get; set; }

        public int ProgressEstimate { get; set; }
        
        public UserResponseDTO Owner { get; set; }
    }
}