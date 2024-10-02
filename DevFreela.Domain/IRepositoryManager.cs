using DevFreela.Domain.Interfaces.Repositories;

namespace DevFreela.Domain
{
    public interface IRepositoryManager : IDisposable
    {
        IUserRepository UserRepository { get; }
        ISkillRepository SkillRepository { get; }
        IProjectRepository ProjectRepository { get; }

        Task Save();
    }
}
