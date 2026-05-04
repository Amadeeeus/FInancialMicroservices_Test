using FinanceService.Application.Commands;
using FluentValidation;

namespace FinanceService.Api.Validators;

public class GetUserWithFavouriteRateCommandValidator 
    : AbstractValidator<GetUserWithFavouriteRateCommand>
{
    public GetUserWithFavouriteRateCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId не может быть пустым");
    }
}