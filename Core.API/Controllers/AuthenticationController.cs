using Core.Application.Authentication.Common;
using Core.Application.Authentication.Queries.Login;
using Core.Contracts.Authentication;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;
        public AuthenticationController(ISender mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Login to your account
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            LoginQuery query = new(request.Email, request.Password);

            ErrorOr<AuthenticationResponse> response = await _mediator.Send(query, cancellationToken);

            return response.Match(
                response => Ok(response), 
                error => Problem(error));
        }
    }
}
