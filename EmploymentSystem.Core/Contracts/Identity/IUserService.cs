using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Dto.Account;
using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.ViewModel.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Core.Contracts.Identity
{
    public interface IUserService
    {
        Task<bool> CheckEmailIsExistsAsync(string email);
        Task<bool> CheckUserNameIsExistsAsync(string username);
        Task<bool> CheckPhoneIsExistsAsync(string phone);
        Task<CommonResponse<string>> CreateUserAsync(ApplicationUserVM applicationUserVM);
        Task CreateDefaultUsersAsync();
        Task<CommonResponse<LoginUserDto>> Login(LoginVM model);
        Task<ApplicationUser> GetCurrentUser();
        Task<bool> CkeckUserInRole(ApplicationUser user, string role);
    }
}
