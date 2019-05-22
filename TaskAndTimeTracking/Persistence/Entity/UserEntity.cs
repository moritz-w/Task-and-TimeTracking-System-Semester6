using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskAndTimeTracking.Persistence.Entity
{
    public class UserEntity : BaseEntity
    {
        protected UserEntity()
        {
            
        }

        [Required, MinLength(2), MaxLength(50)]
        public string FirstName { get; set; }
        
        [Required, MinLength(2), MaxLength(50)]
        public string LastName { get; set; }
        
        [Required, EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
        
        [Required]
        public string Salt { get; set; }

        [Range(1, 10)]
        public int AuthorizationLevel { get; set; }
        
        public virtual ICollection<TodoUserAssignment> TodoUserAssignments { get; set; }
        
        public virtual ICollection<ProjectUserAssignment> ProjectUserAssignments { get; set; }
    }
}