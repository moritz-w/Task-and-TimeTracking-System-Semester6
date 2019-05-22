using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskAndTimeTracking.Persistence.Entity
{
    public class TodoEntity : BaseEntity
    {

        protected TodoEntity()
        {
            
        }
        
        [Required, MinLength(2), MaxLength(100)]
        public String Name { get; set; }
        
        [MinLength(5), MaxLength(500)]
        public String TaskDescription { get; set; }
        
        public bool Done { get; set; } 
        
        public ProjectEntity Project{ get; set; }

        public virtual ICollection<WorkProgressEntity> WorkProgressEntities { get; set; }
        
        public virtual ICollection<TodoUserAssignment> TodoUserAssignments { get; set; }
    }
}