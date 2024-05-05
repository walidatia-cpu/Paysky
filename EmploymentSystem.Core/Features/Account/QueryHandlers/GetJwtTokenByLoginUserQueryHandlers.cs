using EmploymentSystem.Core.Contracts.Identity;
using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Dto.Account;
using EmploymentSystem.Core.Features.Account.Queries;
using EmploymentSystem.Core.ViewModel.Identity;
using MediatR;

namespace EmploymentSystem.Core.Features.Account.QueryHandlers
{
    public class GetJwtTokenByLoginUserQueryHandlers : IRequestHandler<GetJwtTokenByLoginUserQuery, CommonResponse<LoginUserDto>>
    {
        #region fields
        private readonly IUserService userService;
        #endregion

        #region ctor
        public GetJwtTokenByLoginUserQueryHandlers(IUserService userService)
        {
            this.userService = userService;
        }
        #endregion

        #region methods
        public async Task<CommonResponse<LoginUserDto>> Handle(GetJwtTokenByLoginUserQuery request, CancellationToken cancellationToken)
        {
            return await userService.Login(new LoginVM { UserName = request.UserName, Password = request.Password });
        }
        #endregion
    }
}
