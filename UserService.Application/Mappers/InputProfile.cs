using AutoMapper;
using User.UserService.Application.Commands;
using User.UserService.Application.Dtos;
using UserServiceApplication.Dtos;
using UserServiceApplication.Queries;

namespace UserServiceApplication.Mappers;

public class InputProfile : Profile
{
    public InputProfile()
    {
        CreateMap<GetUserByIdDto, GetUserByIdQuery>();
        CreateMap<CreateUserDto, CreateUserCommand>();
    }
}