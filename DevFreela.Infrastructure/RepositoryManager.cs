using DevFreela.Domain;
using DevFreela.Domain.Interfaces.Repositories;
using DevFreela.Infrastructure.Context;

namespace DevFreela.Infrastructure
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly DevFreelaDbContext devFreelaDbContext;

        private bool disposed;

        public IUserRepository UserRepository { get; }
        public ISkillRepository SkillRepository { get; }
        public IProjectRepository ProjectRepository { get; }

        public RepositoryManager(DevFreelaDbContext devFreelaDbContext,
                                IUserRepository userRepository,
                                ISkillRepository skillRepository,
                                IProjectRepository projectRepository
                                )
        {
            this.devFreelaDbContext = devFreelaDbContext;
            UserRepository = userRepository;
            SkillRepository = skillRepository;
            ProjectRepository = projectRepository;
        }

        public async Task Save()
        {
            await devFreelaDbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    devFreelaDbContext.Dispose();
                    UserRepository.Dispose();
                    SkillRepository.Dispose();
                    ProjectRepository.Dispose();
                   
                }
                disposed = true;
            }
        }

        ~RepositoryManager()
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
