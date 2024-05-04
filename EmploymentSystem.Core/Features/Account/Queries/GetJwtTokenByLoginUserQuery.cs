using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Dto.Account;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Core.Features.Account.Queries
{
    public class GetJwtTokenByLoginUserQuery:IRequest<CommonResponse<LoginUserDto>>
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}

