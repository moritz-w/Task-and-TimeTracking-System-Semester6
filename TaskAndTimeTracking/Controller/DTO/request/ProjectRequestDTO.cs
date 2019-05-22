using System.ComponentModel.DataAnnotations;

namespace TaskAndTimeTracking.Controller.DTO.request
{
    public class ProjectRequestDTO : BaseDTO
    {
        public ProjectRequestDTO()
        {
        }
        
        [Required, MinLength(2), MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(500)]
        public string Description { get; set; }
        
        [Range(0, 100)]
        public int ProgressEstimate { get; set; }
        
        [Required, Range(1, int.MaxValue)]
        public int Owner { get; set; }
    }
}