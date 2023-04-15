namespace WebApplication4.Models
{
    public class ToDoContext
    {
        public List<Category>? Categories { get; set; }
        public List<ToDoTask>? ToDoTasks { get; set; }
    }
}
