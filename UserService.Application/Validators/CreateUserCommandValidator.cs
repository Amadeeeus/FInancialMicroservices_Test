using FluentValidation;
using User.UserService.Application.Commands;

namespace UserServiceApplication.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Имя не может быть пустым")
            .MaximumLength(100)
            .WithMessage("Имя не может быть длиннее 100 символов");
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Пароль не может быть пустым")
            .MinimumLength(6)
            .WithMessage("Пароль должен быть не менее 6 символов");
    }
}