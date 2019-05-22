using System.ComponentModel.DataAnnotations.Schema;

namespace TaskAndTimeTracking.Persistence.Entity
{
    public class TodoUserAssignment
    {
        public TodoUserAssignment()
        {
            
        }
        
        [Column(Order = 0)]
        [ForeignKey("Todos")]
        public int TodoId { get; set; }
        
        [Column(Order = 1)]
        [ForeignKey("Users")]
        public int UserId { get; set; }
        
        public TodoEntity TodoEntity { get; set; }
        public UserEntity UserEntity { get; set; }
    }
}