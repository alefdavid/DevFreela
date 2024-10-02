using AutoMapper;
using DevFreela.Domain.DTOs;
using DevFreela.Domain.Entities;

namespace DevFreela.Infrastructure.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();

            CreateMap<UserDTO, User>();

            CreateMap<CreateUserDTO, User>();

            CreateMap<User, CreateUserDTO>();

            CreateMap<UpdateUserDTO, User>();
        }
    }
}
