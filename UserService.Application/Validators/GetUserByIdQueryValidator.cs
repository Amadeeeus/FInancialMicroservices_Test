using FluentValidation;
using UserServiceApplication.Queries;

namespace UserServiceApplication.Validators;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId не может быть пустым");
    }
}