using Microsoft.AspNetCore.Identity;

namespace ActionBoard.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Add custom user properties here
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation property for todos
        public virtual ICollection<Todo> Todos { get; set; } = new List<Todo>();
    }
} 