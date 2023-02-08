﻿using Core.Application.Users.Common;
using ErrorOr;
using MediatR;

namespace Core.Application.Users.Queries.GetUsers;

public record GetUsersQuery (int PageNumber, int PageSize) : IRequest<ErrorOr<List<UserResponse>>>;
