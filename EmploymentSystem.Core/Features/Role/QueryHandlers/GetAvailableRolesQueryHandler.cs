using EmploymentSystem.Core.Contracts.Identity;
using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Dto.Role;
using EmploymentSystem.Core.Features.CommandHandlers.Queries;
using MediatR;

namespace EmploymentSystem.Core.Features.CommandHandlers.QueryHandlers
{
    public class GetAvailableRolesQueryHandler : IRequestHandler<GetAvailableRolesQuery, CommonResponse<List<RoleDto>>>
    {
        #region fields
        private readonly IRoleService roleService;
        #endregion

        #region ctor
        public GetAvailableRolesQueryHandler(IRoleService roleService)
        {
            this.roleService = roleService;
        }
        #endregion

        #region methods
        public async Task<CommonResponse<List<RoleDto>>> Handle(GetAvailableRolesQuery request, CancellationToken cancellationToken)
        {
            return await roleService.GetAvailableRegisterRoles();
        }
        #endregion
    }
}
