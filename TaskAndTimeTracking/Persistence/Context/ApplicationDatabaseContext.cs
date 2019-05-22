using Microsoft.EntityFrameworkCore;
using TaskAndTimeTracking.Persistence.Entity;

namespace TaskAndTimeTracking.Persistence.Context
{
    public class ApplicationDatabaseContext : DbContext
    {
        public DbSet<ProjectEntity> Projects{ get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TodoEntity> Todos { get; set; }
        public DbSet<WorkProgressEntity> WorkProgress { get; set; }
       
        
        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) : base (options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoUserAssignment>().HasKey(c => new {c.TodoId, c.UserId});
            modelBuilder.Entity<TodoUserAssignment>()
                .HasOne(pt => pt.TodoEntity)
                .WithMany(p => p.TodoUserAssignments)
                .HasForeignKey(pt => pt.TodoId);
            modelBuilder.Entity<TodoUserAssignment>()
                .HasOne(pt => pt.UserEntity)
                .WithMany(p => p.TodoUserAssignments)
                .HasForeignKey(pt => pt.UserId);
            
           modelBuilder.Entity<ProjectEntity>().HasMany(pt => pt.Todos);

           modelBuilder.Entity<ProjectUserAssignment>().HasKey(c => new {c.ProjectId, c.UserId});
            modelBuilder.Entity<ProjectUserAssignment>()
                .HasOne(pt => pt.UserEntity)
                .WithMany(p => p.ProjectUserAssignments)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(pt => pt.UserId);
            modelBuilder.Entity<ProjectUserAssignment>()
                .HasOne(pt => pt.ProjectEntity)
                .WithMany(p => p.ProjectUserAssignments)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(pt => pt.ProjectId);
        }
    }
}