using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<ToDoTask> Tasks { get; set;}

        public Category()
        {
            Tasks = new HashSet<ToDoTask>();
        }
    }
}
