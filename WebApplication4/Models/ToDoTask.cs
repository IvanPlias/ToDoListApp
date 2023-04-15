using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class ToDoTask
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; } = null!;
        public DateTime Deadline { get; set; }
        public bool IsComplete  { get; set; }
        public int CategoryId { get; set; }
    }
}
