using EmploymentSystem.Core.Constant;
using EmploymentSystem.Core.Contracts.Identity;
using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Dto.Role;
using EmploymentSystem.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.BLL.Services.Identity
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleService(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task CreateRoleAsync(string roleName)
        {
            await _roleManager.CreateAsync(new ApplicationRole { Name = roleName });
        }
        public async Task CreateDefaultRolesAsync()
        {
            if (!await RoleExistsAsync(Role.SuperAdmin.ToString()))
                await CreateRoleAsync(Role.SuperAdmin.ToString());

            if (!await RoleExistsAsync(Role.Employer.ToString()))
                await CreateRoleAsync(Role.Employer.ToString());

            if (!await RoleExistsAsync(Role.Applicant.ToString()))
                await CreateRoleAsync(Role.Applicant.ToString());
        }

        public async Task<CommonResponse<List<RoleDto>>> GetAvailableRegisterRoles()
        {
            var _roles = _roleManager.Roles
                 .Where(c => c.Name != Role.SuperAdmin.ToString())
                 .Select(c =>
                      new RoleDto
                      {
                          RoleId = c.Id,
                          RoleName = c.Name!
                      }).ToList();
            return new CommonResponse<List<RoleDto>> { Data = _roles, RequestStatus = RequestStatus.Success };
        }

        public async Task<string?> GetRoleNameById(string RoleId)
        {
            return _roleManager.Roles.FirstOrDefault(c => c.Id == RoleId)?.Name ?? null;
        }
    }
}
