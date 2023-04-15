using System.ComponentModel.DataAnnotations;

namespace WebApplication4.ViewModels
{
    public class AddTaskViewModel
    {
        public DateTime? Deadline { get; set; }
        [MinLength(1, ErrorMessage = "The minimum length 1 characters.")]
        [Required(ErrorMessage = "No Description")]
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
    }
}
