using FluentValidation;

namespace Core.Application.Users.Commands.AddUser;
public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
	public AddUserCommandValidator()
	{
		RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
		RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email address is required")
            .When(x => !string.IsNullOrEmpty(x.Email))
            .EmailAddress().WithMessage("A valid email address is required");
    }
}
