using AutoMapper;
using DevFreela.Domain.DTOs;
using DevFreela.Domain.Entities;

namespace DevFreela.Infrastructure.Profiles
{
    public class SkillProfile : Profile
    {
        public SkillProfile()
        {
            CreateMap<Skill, SkillDTO>();

            CreateMap<SkillDTO, Skill>();

            CreateMap<CreateSkillDTO, Skill>();

            CreateMap<Skill, CreateSkillDTO>();

            CreateMap<UpdateSkillDTO, Skill>();
        }
    }
}
