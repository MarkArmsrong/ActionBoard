using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ActionBoard.Models;

namespace ActionBoard.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Todo>()
                .Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Todo>()
                .Property(t => t.Description)
                .HasMaxLength(500);

            modelBuilder.Entity<Todo>()
                .Property(t => t.Status)
                .HasMaxLength(20)
                .HasDefaultValue("todo");

            // Configure Todo-User relationship
            modelBuilder.Entity<Todo>()
                .HasOne(t => t.User)
                .WithMany(u => u.Todos)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 