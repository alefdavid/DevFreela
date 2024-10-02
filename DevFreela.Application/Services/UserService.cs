using AutoMapper;
using DevFreela.Application.Interfaces.Services;
using DevFreela.Domain;
using DevFreela.Domain.DTOs;
using DevFreela.Domain.Entities;

namespace DevFreela.Application.Services
{
    public class UserService : IUserService
    {
        public readonly IRepositoryManager _repositoryManager;

        public readonly IMapper _mapper;

        private bool disposed;

        public UserService (IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        public async Task<UserDTO> GetById(int id)
        {
            if (id == null)
                throw new Exception();

            var listUser = await _repositoryManager.UserRepository.GetAll();
            var user = listUser.Where(x => x.Id == id).FirstOrDefault();

            var userDTO = _mapper.Map<UserDTO>(user);

            return userDTO;
        }

        public async Task<List<UserDTO>> GetAll()
        {
            var listUsers = await _repositoryManager.UserRepository.GetAll();
            var listUsersDTO = _mapper.Map<List<UserDTO>>(listUsers);

            return listUsersDTO;
        }

        public async Task<CreateUserDTO> Post(CreateUserDTO createUserDTO)
        {
            if (createUserDTO == null)
                throw new ArgumentNullException(nameof(createUserDTO));

            var user = _mapper.Map<CreateUserDTO, User>(createUserDTO);

            _repositoryManager.UserRepository.Add(user);
            await _repositoryManager.Save();

            return _mapper.Map<User, CreateUserDTO>(user);
        }

        public async Task<bool> Put(UpdateUserDTO updateUserDTO, int id)
        {
            if (updateUserDTO == null)
                throw new ArgumentNullException(nameof(updateUserDTO));

            var user = await _repositoryManager.UserRepository.GetAll();
            var userExists = user.Where(x => x.Id == id).FirstOrDefault();

            if (userExists == null)
                throw new ArgumentNullException($"Não existe: {userExists}.");

            _mapper.Map(updateUserDTO, userExists);

            _repositoryManager.UserRepository.Put(userExists);

            await _repositoryManager.Save();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var user = await _repositoryManager.UserRepository.GetAll();
            var returnUserRemove = user.Where(x => x.Id == id).FirstOrDefault();

            if (returnUserRemove == null)
                throw new ArgumentNullException($"Não existe: {id}.");

            await _repositoryManager.UserRepository.Delete(returnUserRemove.Id);

            await _repositoryManager.Save();

            return true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _repositoryManager.Dispose();
                }

                disposed = true;
            }
        }

        ~UserService()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

