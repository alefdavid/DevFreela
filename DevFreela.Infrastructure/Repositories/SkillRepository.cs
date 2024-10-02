using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces.Repositories;
using DevFreela.Infrastructure.Context;

namespace DevFreela.Infrastructure.Repositories
{
    public class SkillRepository : Repository<Skill>, ISkillRepository
    {
        public SkillRepository(DevFreelaDbContext dbContext) : base(dbContext) { }
    }
}
