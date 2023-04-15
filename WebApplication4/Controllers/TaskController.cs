using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
        public Task<IActionResult> Index()
        {
            return Task.FromResult<IActionResult>(View(new IndexViewModel()));
        }
        [HttpPost]
        public Task<IActionResult> Delete(IdTaskViewModel IdTaskViewModel)
        {
            TaskRepository.DeleteTaskAsync(IdTaskViewModel);
            return Task.FromResult<IActionResult>(RedirectToAction("Index"));
        }
        [HttpPost]
        public Task<IActionResult> Complete(IsCompleteTaskViewModel IsCompleteTaskViewModel)
        {
            TaskRepository.CompleteTaskAsync(IsCompleteTaskViewModel);
            return Task.FromResult<IActionResult>(RedirectToAction("Index"));
        }
        [HttpPost]
        public Task<IActionResult> Create(AddTaskViewModel AddTaskViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Task.FromResult<IActionResult>(RedirectToAction("Index"));
            }
            var task = _mapper.Map<ToDoTask>(AddTaskViewModel);
            TaskRepository.AddTaskAsync(task);
            return Task.FromResult<IActionResult>(RedirectToAction("Index"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}