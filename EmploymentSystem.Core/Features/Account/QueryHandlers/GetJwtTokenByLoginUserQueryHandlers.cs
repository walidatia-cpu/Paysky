using EmploymentSystem.Core.Constant;
using EmploymentSystem.Core.Contracts.Identity;
using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Dto.Account;
using EmploymentSystem.Core.Features.Account.Queries;
using EmploymentSystem.Core.ViewModel.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Core.Features.Account.QueryHandlers
{
    public class GetJwtTokenByLoginUserQueryHandlers : IRequestHandler<GetJwtTokenByLoginUserQuery, CommonResponse<LoginUserDto>>
    {
        private readonly IUserService userService;

        public GetJwtTokenByLoginUserQueryHandlers(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<CommonResponse<LoginUserDto>> Handle(GetJwtTokenByLoginUserQuery request, CancellationToken cancellationToken)
        {
            return await userService.Login(new LoginVM { UserName=request.UserName,Password=request.Password });
        }
    }
}
