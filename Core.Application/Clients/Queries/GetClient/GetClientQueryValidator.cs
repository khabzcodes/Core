using FluentValidation;

namespace Core.Application.Clients.Queries.GetClient;

public class GetClientQueryValidator : AbstractValidator<GetClientQuery>
{
	public GetClientQueryValidator()
	{
		RuleFor(x => x.Id).NotEmpty().WithMessage("Client Id is required");
	}
}
