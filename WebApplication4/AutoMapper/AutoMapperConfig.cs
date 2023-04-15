using AutoMapper;
using WebApplication4.Models;
using WebApplication4.ViewModels;

namespace WebApplication4.AutoMapper
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig() 
        { 
            CreateMap<AddTaskViewModel,ToDoTask>().ReverseMap();
        }
    }
}
