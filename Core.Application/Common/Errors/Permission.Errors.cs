using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Common.Errors;

public static class PermissionErrors
{
    public static Error NotFound(string permission) => Error.Validation(
        code: "Permissions.NotFound",
        description: $"{permission} permission doesn't exist"
        );
}
