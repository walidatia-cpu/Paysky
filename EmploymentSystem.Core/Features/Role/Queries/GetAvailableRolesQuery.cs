using EmploymentSystem.Core.Dto.Account;
using EmploymentSystem.Core.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmploymentSystem.Core.Dto.Role;

namespace EmploymentSystem.Core.Features.CommandHandlers.Queries
{
    public class GetAvailableRolesQuery:IRequest<CommonResponse<List<RoleDto>>>
    {
    }
}
