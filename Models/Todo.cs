using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActionBoard.Models
{
    public class Todo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string Status { get; set; } = "todo"; // todo, inprogress, completed

        // Foreign key for user
        public string? UserId { get; set; }

        // Navigation property for user
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }
    }
} 