using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Dto.Account;
using EmploymentSystem.Core.Features.Account.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.Controllers.Account.Authentication
{
    [Route("api/v1/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("login")]
        public async Task<CommonResponse<LoginUserDto>> Login([FromBody] GetJwtTokenByLoginUserQuery model)
        {
            return await mediator.Send(model);
        }
    }
}
