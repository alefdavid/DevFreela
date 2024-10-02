using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces.Repositories;
using DevFreela.Infrastructure.Context;

namespace DevFreela.Infrastructure.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(DevFreelaDbContext dbContext) : base(dbContext) { }              
    }
}
