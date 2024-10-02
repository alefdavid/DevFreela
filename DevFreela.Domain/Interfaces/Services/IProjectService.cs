using DevFreela.Domain.DTOs;

namespace DevFreela.Application.Interfaces.Services
{
    public interface IProjectService : IService
    {
        Task<CreateProjectDTO> Post(CreateProjectDTO createProjectDTO);
        Task<bool> Put(UpdateProjectDTO updateProjectDTO, int id);
        Task<ProjectDTO> GetById(int id);
        Task<List<ProjectDTO>> GetAll();
        Task<bool> Delete(int id);
    }
}