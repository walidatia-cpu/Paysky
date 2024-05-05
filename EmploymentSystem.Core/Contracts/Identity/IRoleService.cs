using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Dto.Role;

namespace EmploymentSystem.Core.Contracts.Identity
{
    public interface IRoleService
    {
        Task<bool> RoleExistsAsync(string roleName);
        Task<string?> GetRoleNameById(string RoleId);
        Task CreateRoleAsync(string roleName);
        Task CreateDefaultRolesAsync();
        Task<CommonResponse<List<RoleDto>>> GetAvailableRegisterRoles();
    }
}
