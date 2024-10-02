using DevFreela.Domain.DTOs;

namespace DevFreela.Application.Interfaces.Services
{
    public interface IUserService : IService
    {
        Task<CreateUserDTO> Post(CreateUserDTO createUserDTO);
        Task<bool> Put(UpdateUserDTO updateUserDTO, int id);
        Task<UserDTO> GetById(int id);
        Task<List<UserDTO>> GetAll();
        Task<bool> Delete(int id);
    }
}