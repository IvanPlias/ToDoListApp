using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication4.Models;
using WebApplication4.Repositories;

namespace WebApplication4.ViewModels
{
    public class IndexViewModel
    {
        public ToDoContext? Context { get; set; }
        public IEnumerable<SelectListItem> CategoriesList { get; set; }
        public AddTaskViewModel? AddTaskViewModel { get; set; }
        public IdTaskViewModel? IdTaskViewModel { get; set; }
        public IsCompleteTaskViewModel? IsCompleteTaskViewModel { get; set; }
        public IndexViewModel() 
        {
            Context = new ToDoContext
            {
                Categories = CategoryRepository.LoadCategories(),
                ToDoTasks = TaskRepository.LoadTasks()
            };
            CategoriesList = CategoryRepository.ListCategories();

        }
    }
}
