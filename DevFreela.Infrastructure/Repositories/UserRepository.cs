using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces.Repositories;
using DevFreela.Infrastructure.Context;

namespace DevFreela.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DevFreelaDbContext dbContext) : base(dbContext) { }              
    }
}
