using AutoMapper;
using User.UserService.Application.Commands;
using User.UserService.Application.Dtos;
using UserService.Domain.Entities;
using UserServiceApplication.Commands;
using UserServiceApplication.Dtos;
using UserServiceApplication.Queries;

namespace UserServiceApplication.Mappers;

public class InputProfile : Profile
{
    public InputProfile()
    {
        CreateMap<GetUserByIdDto, GetUserByIdQuery>();
        
        CreateMap<CreateUserDto, CreateUserCommand>();
        
        CreateMap<CreateUserDto, UpdateUserCommand>();
        
        CreateMap<AuthentificationUserDto,  AuthentificationUserCommand>();
        
        CreateMap<UserEntity,GetUserByIdOutDto>()
            .ForMember(x=>x.Id,  opt => opt.MapFrom(x=>x.Id))
            .ForMember(x=>x.Name,  opt => opt.MapFrom(x=>x.Name))
            .ForMember(x=>x.Favourites,  opt=>opt.MapFrom(x=>x.Favourites));
    }
}