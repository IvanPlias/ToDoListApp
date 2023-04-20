using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using ToDoListApp.XmlStorage;
using WebApplication4.Models;
using WebApplication4.Repositories;

namespace WebApplication4.ViewModels
{
    public class IndexViewModel
    {
        public string Storage { get; set; } = "MSSQL";
        public ToDoContext? Context { get; set; }
        public IEnumerable<SelectListItem>? CategoriesList { get; set; }
        public AddTaskViewModel? AddTaskViewModel { get; set; }
        public IdTaskViewModel? IdTaskViewModel { get; set; }
        public IsCompleteTaskViewModel? IsCompleteTaskViewModel { get; set; }
        public IndexViewModel() { }
        public IndexViewModel(string storagetmp)
        {
            if(storagetmp!=null)
                Storage = storagetmp;
            if (Storage == "MSSQL")
            {
                Context = new ToDoContext
                {
                    Categories = CategoryRepository.LoadCategories(),
                    ToDoTasks = TaskRepository.LoadTasks()
                };
                CategoriesList = CategoryRepository.ListCategories();
            }
            else if (Storage == "Xmlstorage")
            {
                Context = new ToDoContext
                {
                    Categories = XmlToDoListRepository.LoadCategories(),
                    ToDoTasks = XmlToDoListRepository.LoadTasks()
                };
                CategoriesList = XmlToDoListRepository.ListCategories();
            }
        }
    }
}
