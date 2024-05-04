using EmploymentSystem.Core.Dto.Account;
using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Features.Account.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmploymentSystem.Core.Features.Account.Commands;
using EmploymentSystem.Core.Contracts.Identity;
using EmploymentSystem.Core.ViewModel.Identity;
using EmploymentSystem.Core.Constant;

namespace EmploymentSystem.Core.Features.Account.CommandHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CommonResponse<string>>
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;

        public CreateUserCommandHandler(IUserService userService, IRoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }
        public async Task<CommonResponse<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var _role = await roleService.GetRoleNameById(request.RoleId);
            if (_role == null)
            {
                return new CommonResponse<string> { Message = "invalid Rolde Id ", RequestStatus = RequestStatus.BadRequest };
            }
            var checkEmail = await userService.CheckEmailIsExistsAsync(request.Email);
            if (checkEmail)
            {
                return new CommonResponse<string> { Message = $" email {request.Email} is already taken ", RequestStatus = RequestStatus.BadRequest };
            }
            var checkPhone = await userService.CheckPhoneIsExistsAsync(request.PhoneNumber);
            if (checkPhone)
            {
                return new CommonResponse<string> { Message = $"The phone {request.PhoneNumber} is already taken ", RequestStatus = RequestStatus.BadRequest };
            }

            var _user = new ApplicationUserVM
            {
                Email = request.Email,
                FullName = request.FullName,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber,
                RoleName = _role,
                UserName = request.UserName
            };
            return await userService.CreateUserAsync(_user);
        }
    }
}
