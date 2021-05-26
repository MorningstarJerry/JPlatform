using FluentValidation;

namespace Application.ApplicationUser.Queries.GetToken
{
    public class GetTokenQueryValidator : AbstractValidator<GetTokenQuery>
    {
        public GetTokenQueryValidator()
        {
            RuleFor(v => v.Phone)
                .MaximumLength(100).WithMessage("Phone must not exceed 100 characters.")
                .NotEmpty().WithMessage("Phone is required.");

            RuleFor(v => v.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
