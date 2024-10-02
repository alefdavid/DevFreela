using AutoMapper;

namespace DevFreela.Infrastructure.Profiles
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserProfile());
                cfg.AddProfile(new SkillProfile());
                cfg.AddProfile(new ProjectProfile());
            });

            return config.CreateMapper();
        }
    } 
}
