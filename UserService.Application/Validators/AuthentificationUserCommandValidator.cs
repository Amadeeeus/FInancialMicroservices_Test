using FluentValidation;
using UserServiceApplication.Commands;

namespace UserServiceApplication.Validators;

public class AuthentificationUserCommandValidator : AbstractValidator<AuthentificationUserCommand>
{
    public AuthentificationUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Имя не может быть пустым");
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Пароль не может быть пустым");
    }
}