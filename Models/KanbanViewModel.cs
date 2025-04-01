using System.Collections.Generic;

namespace ActionBoard.Models
{
    public class KanbanViewModel
    {
        public List<Todo> Todo { get; set; } = new();
        public List<Todo> InProgress { get; set; } = new();
        public List<Todo> Completed { get; set; } = new();
    }
} 