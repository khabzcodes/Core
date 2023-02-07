using Core.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Core.Infrastructure.Authentication;

public class HasPermissionAttribute : AuthorizeAttribute
{
	public HasPermissionAttribute(Permissions permission)
		:base(policy: permission.ToString())
	{

	}
}
