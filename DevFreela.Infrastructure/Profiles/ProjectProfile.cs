using AutoMapper;
using DevFreela.Domain.DTOs;
using DevFreela.Domain.Entities;

namespace DevFreela.Infrastructure.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDTO>();

            CreateMap<ProjectDTO, Project>();

            CreateMap<CreateProjectDTO, Project>();

            CreateMap<Project, CreateProjectDTO>();

            CreateMap<UpdateProjectDTO, Project>();
        }
    }
}
