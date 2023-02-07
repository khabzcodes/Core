using FluentValidation;

namespace Core.Application.UserPermissions.Commands;

public class AddUserPermissionsCommandValidator : AbstractValidator<AddUserPermissionsCommand>
{
	public AddUserPermissionsCommandValidator()
	{

	}
}
