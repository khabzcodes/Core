﻿using Microsoft.AspNetCore.Authorization;

namespace Core.Infrastructure.Authentication;

public class PermissionRequirement : IAuthorizationRequirement
{
	public PermissionRequirement(string permission)
	{
		Permission = permission;
	}

	public string Permission { get; }
}
