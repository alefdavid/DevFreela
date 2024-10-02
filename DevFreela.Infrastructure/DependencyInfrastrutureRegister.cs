using DevFreela.Domain;
using DevFreela.Domain.Interfaces.Repositories;
using DevFreela.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Infrastructure
{
    public static class DependencyInfrastrutureRegister
    {
        public static IServiceCollection RegisterInfrastrutureDependencies(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
           
            return services;
        }
    }
}
