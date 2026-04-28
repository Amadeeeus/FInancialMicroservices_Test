using AutoMapper;
using User.UserService.Application.Commands;
using User.UserService.Application.Dtos;
using User.UserService.Application.Queries;

namespace UserServiceApplication.Mappers;

public class InputProfile : Profile
{
    public InputProfile()
    {
        CreateMap<GetUserByIdDto, GetUserByIdQuery>();
        CreateMap<CreateUserDto, CreateUserCommand>();
    }
}