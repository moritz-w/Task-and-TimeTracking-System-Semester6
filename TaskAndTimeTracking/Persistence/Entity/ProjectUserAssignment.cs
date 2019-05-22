using System.ComponentModel.DataAnnotations.Schema;

namespace TaskAndTimeTracking.Persistence.Entity
{
    public class ProjectUserAssignment
    {
        public ProjectUserAssignment()
        {
            
        }
        
        [Column(Order = 0)]
        [ForeignKey("Projects")]
        public int ProjectId { get; set; }
        
        [Column(Order = 1)]
        [ForeignKey("Users")]
        public int UserId { get; set; }
        
        public ProjectEntity ProjectEntity { get; set; }
        public UserEntity UserEntity { get; set; }
    }
}