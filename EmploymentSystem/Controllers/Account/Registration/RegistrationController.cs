using EmploymentSystem.Core.Dto.Account;
using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Features.Account.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmploymentSystem.Core.Features.Account.Commands;

namespace EmploymentSystem.Controllers.Account.Registration
{
    [Route("api/v1/Registration")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IMediator mediator;

        public RegistrationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<CommonResponse<string>> CreateUser([FromBody] CreateUserCommand model)
        {
            return await mediator.Send(model);
        }
    }
}
