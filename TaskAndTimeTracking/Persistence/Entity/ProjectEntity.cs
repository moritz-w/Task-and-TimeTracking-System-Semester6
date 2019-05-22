using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskAndTimeTracking.Persistence.Entity
{
    public class ProjectEntity : BaseEntity
    {
        protected ProjectEntity()
        {
            
        }
        
        [Required, MinLength(2), MaxLength(200)]
        public String Name { get; set; }
        
        [MaxLength(500)]
        public String Description { get; set; }

        [Range(0, 100)] 
        public int ProgressEstimate { get; set; }

        [Required]
        public virtual UserEntity Owner { get; set; }

        public ICollection<ProjectUserAssignment> ProjectUserAssignments { get; set; }

        public virtual ICollection<TodoEntity> Todos { get; set; }
    }
}