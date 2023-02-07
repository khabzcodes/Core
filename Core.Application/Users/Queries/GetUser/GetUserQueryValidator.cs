using FluentValidation;

namespace Core.Application.Users.Queries.GetUser;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
	public GetUserQueryValidator()
	{
		RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
	}
}
