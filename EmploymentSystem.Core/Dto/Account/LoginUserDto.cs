using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Core.Dto.Account
{
    public class LoginUserDto
    {
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
