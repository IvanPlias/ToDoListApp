using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoListApp.XmlStorage;
using WebApplication4.Models;
using WebApplication4.Repositories;
using WebApplication4.ViewModels;

namespace WebApplication4.Controllers
{
    public class TaskController : Controller
    {
        private readonly IMapper _mapper;
        public TaskController(IMapper mapper)
        {
            _mapper = mapper;
        }
        public Task<IActionResult> Index(string storage)
        {
            IndexViewModel indexViewModel = new(storage);
            return Task.FromResult<IActionResult>(View(indexViewModel));
        }
        [HttpPost]
        public Task<IActionResult> DeleteTaskFrom(IdTaskViewModel IdTaskViewModel, IndexViewModel indexViewModel)
        {
            switch (indexViewModel.Storage)
            {
                case "MSSQL":
                    TaskRepository.DeleteTaskAsync(IdTaskViewModel);
                    break;
                case "Xmlstorage":
                    XmlToDoListRepository.DeleteTask(IdTaskViewModel);
                    break;
            }
            return Task.FromResult<IActionResult>(RedirectToAction("Index", new RouteValueDictionary(new { Controller = "Task", Action = "Index", storage = indexViewModel.Storage })));
        }
        [HttpPost]
        public Task<IActionResult> TaskComplete(IsCompleteTaskViewModel IsCompleteTaskViewModel, IndexViewModel indexViewModel)
        {
            switch (indexViewModel.Storage)
            {
                case "MSSQL":
                    TaskRepository.CompleteTaskAsync(IsCompleteTaskViewModel);
                    break;
                case "Xmlstorage":
                    XmlToDoListRepository.CompleteTask(IsCompleteTaskViewModel);
                    break;
            }
            return Task.FromResult<IActionResult>(RedirectToAction("Index", new RouteValueDictionary(new { Controller = "Task", Action = "Index", storage = indexViewModel.Storage })));
        }
        [HttpPost]
        public Task<IActionResult> AddTaskTo(AddTaskViewModel AddTaskViewModel, IndexViewModel indexViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Task.FromResult<IActionResult>(RedirectToAction("Index", new RouteValueDictionary(new { Controller = "Task", Action = "Index", storage = indexViewModel.Storage })));
            }
            var task = _mapper.Map<ToDoTask>(AddTaskViewModel);
            switch(indexViewModel.Storage)
            {
                case "MSSQL": TaskRepository.AddTaskAsync(task);
                    break;
                case "Xmlstorage":XmlToDoListRepository.AddTaskXml(task);
                    break;
            }          
            return Task.FromResult<IActionResult>(RedirectToAction("Index", new RouteValueDictionary(new {Controller="Task", Action="Index", storage=indexViewModel.Storage})));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}