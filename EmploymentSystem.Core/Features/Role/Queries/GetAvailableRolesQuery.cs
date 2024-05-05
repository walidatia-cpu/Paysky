using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Dto.Role;
using MediatR;

namespace EmploymentSystem.Core.Features.CommandHandlers.Queries
{
    public class GetAvailableRolesQuery : IRequest<CommonResponse<List<RoleDto>>>
    {
    }
}
