using EmploymentSystem.Core.Dto.Account;
using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Features.Account.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmploymentSystem.Core.Features.CommandHandlers.Queries;
using EmploymentSystem.Core.Dto.Role;
using EmploymentSystem.Core.Contracts.Identity;
using EmploymentSystem.Core.Constant;

namespace EmploymentSystem.Core.Features.CommandHandlers.QueryHandlers
{
    public class GetAvailableRolesQueryHandler : IRequestHandler<GetAvailableRolesQuery, CommonResponse<List<RoleDto>>>
    {
        private readonly IRoleService roleService;

        public GetAvailableRolesQueryHandler(IRoleService roleService)
        {
            this.roleService = roleService;
        }
        public async Task<CommonResponse<List<RoleDto>>> Handle(GetAvailableRolesQuery request, CancellationToken cancellationToken)
        {
            return await roleService.GetAvailableRegisterRoles();
        }
    }
}
