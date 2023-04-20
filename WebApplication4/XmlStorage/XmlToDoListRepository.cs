using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using WebApplication4.Models;
using WebApplication4.ViewModels;

namespace ToDoListApp.XmlStorage
{
    public class XmlToDoListRepository
    {
        private static ICollection<SelectListItem>? CategoriesList;
        public static ICollection<SelectListItem> ListCategories()
        {
            var Categories = LoadCategories();
            CategoriesList = new List<SelectListItem>();
            foreach (var category in Categories)
            {
                CategoriesList.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name,
                });
            }
            return CategoriesList;
        }
        public static List<Category> LoadCategories()
        {
            XDocument? xdoc = null;
            XElement? root = null;
            if (File.Exists("XmlStorage\\XmlStorage.xml"))
            {
                xdoc = XDocument.Load("XmlStorage\\XmlStorage.xml");
                root = xdoc.Element("ToDoList");
            }
            else
            {
                CreateXML();
                xdoc = XDocument.Load("XmlStorage\\XmlStorage.xml");
            }
            List<Category>? categories = root?.Element("Categories")?.Elements("Category").Select(x => new Category
            {
                Id = Convert.ToInt32(x.Attribute("Id")?.Value),
                Name = x.Element("Name").Value
            }).ToList();
            return categories;
        }
        public static List<ToDoTask> LoadTasks()
        {
            XDocument? xdoc = null;
            XElement? root = null;
            if (File.Exists("XmlStorage\\XmlStorage.xml"))
            {
                xdoc = XDocument.Load("XmlStorage\\XmlStorage.xml");
                root = xdoc.Element("ToDoList");
            }
            else
            {
                CreateXML();
                xdoc = XDocument.Load("XmlStorage\\XmlStorage.xml");
            }
            List<ToDoTask>? tasks = root?.Element("ToDoTasks")?.Elements("ToDoTask").Select(x => new ToDoTask
            {
                Id = Convert.ToInt32(x.Attribute("Id")?.Value),
                Description = x.Element("Description")?.Value,
                Deadline = Convert.ToDateTime(x.Element("Deadline")?.Value),
                IsComplete = Convert.ToBoolean(x.Element("IsComplete")?.Value),
                CategoryId = Convert.ToInt32(x.Element("CategoryId")?.Value)
            }).ToList();
            return tasks;
        }
        private static void CreateXML()
        {
            XDocument xdoc = new XDocument();
            XElement? root = new XElement("ToDoList");
            XElement? root1 = new XElement("Categories");
            XElement? root2 = new XElement("ToDoTasks");
            root1.Add(new XElement("Category",
                                     new XAttribute("Id", "1"),
                                     new XElement("Name", "Products")),
                                     new XElement("Category",
                                     new XAttribute("Id", "2"),
                                     new XElement("Name", "Job")),
                                     new XElement("Category",
                                     new XAttribute("Id", "3"),
                                     new XElement("Name", "Study"))
                                     );
            root.Add(root1);
            root.Add(root2);
            xdoc.Add(root);
            xdoc.Save("XmlStorage\\XmlStorage.xml");
        }
        public static void AddTaskXml(ToDoTask task)
        {
            XDocument xdoc = XDocument.Load("XmlStorage\\XmlStorage.xml");
            XElement? root = xdoc.Element("ToDoList");
            XElement? root1 = root.Element("ToDoTasks");
            if (root1 != null)
            {
                root1.Add(new XElement("ToDoTask",
                                     new XAttribute("Id", Guid.NewGuid().GetHashCode()),
                                     new XElement("Description", task.Description?.ToString()),
                                     new XElement("Deadline", task.Deadline.ToShortDateString()),
                                     new XElement("IsComplete", task.IsComplete.ToString()),
                                     new XElement("CategoryId", task.CategoryId.ToString())));
                xdoc.Save("XmlStorage\\XmlStorage.xml");
            }
        }

        public static void CompleteTask(IsCompleteTaskViewModel isCompleteTaskViewModel)
        {
            isCompleteTaskViewModel.IsComplete = !isCompleteTaskViewModel.IsComplete;
            XDocument xdoc = XDocument.Load("XmlStorage\\XmlStorage.xml");
            var task = xdoc.Root?.Descendants("ToDoTasks").Descendants("ToDoTask").FirstOrDefault(x => x.Attribute("Id")?.Value == isCompleteTaskViewModel.Id.ToString());
            if (task != null)
            {
                var complete = task.Element("IsComplete");
                if (complete != null) complete.Value = isCompleteTaskViewModel.IsComplete.ToString();
                xdoc.Save("XmlStorage\\XmlStorage.xml");
            }
        }

        public static void DeleteTask(IdTaskViewModel idTaskViewModel)
        {
            XDocument xdoc = XDocument.Load("XmlStorage\\XmlStorage.xml");
            var task = xdoc.Root?.Descendants("ToDoTasks").Descendants("ToDoTask").FirstOrDefault(x => x.Attribute("Id")?.Value == idTaskViewModel.Id.ToString());
            if (task != null)
            {
                task.Remove();
                xdoc.Save("XmlStorage\\XmlStorage.xml");
            }
        }
    }
}
