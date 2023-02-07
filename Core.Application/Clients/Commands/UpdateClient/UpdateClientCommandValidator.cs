using FluentValidation;

namespace Core.Application.Clients.Commands.UpdateClient;

public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
{
	public UpdateClientCommandValidator()
	{
        RuleFor(x => x.Name).NotEmpty().WithMessage("Client name is required");
        RuleFor(x => x.Sector).NotEmpty().WithMessage("Client sector is required");
        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .WithMessage("Client email address is required")
            .When(x => !string.IsNullOrEmpty(x.EmailAddress))
            .EmailAddress().WithMessage("A valid email address is required");
    }
}
