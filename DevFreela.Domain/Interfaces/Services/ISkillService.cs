using DevFreela.Domain.DTOs;

namespace DevFreela.Application.Interfaces.Services
{

    public interface ISkillService : IService
    {
        Task<CreateSkillDTO> Post(CreateSkillDTO createSkillDTO);
        Task<bool> Put(UpdateSkillDTO updateSkillDTO, int id);
        Task<SkillDTO> GetById(int id);
        Task<List<SkillDTO>> GetAll();
        Task<bool> Delete(int id);
    }
}