using EmploymentSystem.Core.Dto.Account;
using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Features.Account.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmploymentSystem.Core.Features.CommandHandlers.Queries;
using EmploymentSystem.Core.Dto.Role;

namespace EmploymentSystem.Controllers.Role
{
    [Route("api/v1/Role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator mediator;

        public RoleController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("GetAll")]
        public async Task<CommonResponse<List<RoleDto>>> GetAll([FromBody] GetAvailableRolesQuery model)
        {
            return await mediator.Send(model);
        }
    }
}
