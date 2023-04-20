using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dapper;
using WebApplication4.Models;
using WebApplication4.ViewModels;

namespace WebApplication4.Repositories
{
    public class TaskRepository
    {
        private static readonly string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ToDoListDB;Integrated Security=True";
        public  static List<ToDoTask> LoadTasks()
        {
            using SqlConnection conn = new(connStr);
            string query = "select * from tasks";
            var Tasks = conn.Query<ToDoTask>(query);
            return Tasks.ToList();
        }       
        public async static void AddTaskAsync(ToDoTask task)
        {
            string query;
            if (task.Deadline == DateTime.MinValue)
                query = @"insert into Tasks values (@Description,null,0,@CategoryId)";
            else
            query = @"insert into Tasks values (@Description,@Deadline,0,@CategoryId)";
            using SqlConnection conn = new(connStr);
            await conn.ExecuteAsync(query, task);
        }
        public async static void DeleteTaskAsync(IdTaskViewModel taskId)
        {
            string query = @"delete Tasks where Id=@Id";
            using SqlConnection conn = new(connStr);
            await conn.ExecuteAsync(query, taskId);
        }

        public async static void CompleteTaskAsync(IsCompleteTaskViewModel task)
        {
            task.IsComplete = !task.IsComplete;
            string query = @"update Tasks set IsComplete=@IsComplete where Id=@Id";
            using SqlConnection conn = new(connStr);
            await conn.ExecuteAsync(query, task);
        }
    }
}
