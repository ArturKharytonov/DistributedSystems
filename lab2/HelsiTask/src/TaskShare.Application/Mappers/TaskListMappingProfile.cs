using AutoMapper;
using TaskShare.Application.DTO;
using TaskShare.Core.DomainEntities;

namespace TaskShare.Application.Mappers;

public class TaskListMappingProfile : Profile
{
    public TaskListMappingProfile()
    {
        CreateMap<TaskList, TaskListDto>().ReverseMap();
    }
}