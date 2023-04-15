using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using WebApplication4.Models;

namespace WebApplication4.Repositories
{
    public class CategoryRepository
    {
        private static ICollection<SelectListItem>? CategoriesList;
        private static readonly string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ToDoListDB;Integrated Security=True";
        public static List<Category> LoadCategories()
        {
            using SqlConnection conn = new(connStr);
            string query = "select * from Categories";
            var Categories = conn.Query<Category>(query).ToList();
            return Categories.ToList();
        }
        public static ICollection<SelectListItem> ListCategories()
        {
            using SqlConnection conn = new(connStr);
            string query = "select * from Categories";
            var Categories = conn.Query<Category>(query).ToList();
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
    }
}
