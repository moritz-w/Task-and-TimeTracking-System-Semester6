using System;
using System.ComponentModel.DataAnnotations;

namespace TaskAndTimeTracking.Persistence.Entity
{
    public class WorkProgressEntity : BaseEntity
    {
        protected WorkProgressEntity()
        {
            
        }

        [Required]
        public DateTime Start { get; set; }
        
        [Required]
        public DateTime End { get; set; }
        
        [MinLength(2), MaxLength(100)]
        public String Description { get; set; }
        
        public TodoEntity Todo { get; set; }
        
        [Required]
        public virtual UserEntity Person { get; set; } 
    }
}