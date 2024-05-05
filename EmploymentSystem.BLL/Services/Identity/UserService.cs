namespace EmploymentSystem.BLL.Services.Identity
{
    public class UserService : IUserService
    {
        #region fields

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRoleService _roleService;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IOptions<JWTSettings> options;
        private readonly IHttpContextAccessor httpContextAccessor;

        #endregion

        #region ctor

        public UserService(UserManager<ApplicationUser> userManager, IRoleService roleRepository, RoleManager<ApplicationRole> roleManager, IOptions<JWTSettings> options, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _roleService = roleRepository;
            _roleManager = roleManager;
            this.options = options;
            this.httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region methods

        public async Task<bool> CheckEmailIsExistsAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }

        public async Task<CommonResponse<string>> CreateUserAsync(ApplicationUserVM applicationUserVM)
        {
            try
            {


                var user = new ApplicationUser
                {
                    UserName = applicationUserVM.UserName,
                    Email = $"{applicationUserVM.Email}",
                    FullName = applicationUserVM.FullName,
                    CreationDate = DateTime.Now,
                    PhoneNumber = applicationUserVM.PhoneNumber,
                    PhoneNumberConfirmed = true,
                };

                var result = await _userManager.CreateAsync(user, applicationUserVM.Password);

                if (result.Succeeded)
                {
                    if (!await _roleService.RoleExistsAsync(applicationUserVM.RoleName))
                        await _roleService.CreateRoleAsync(applicationUserVM.RoleName);
                    await _userManager.AddToRoleAsync(user, applicationUserVM.RoleName);
                    return new CommonResponse<string> { RequestStatus = RequestStatus.Success, Message = "Success", Data = user.Id };

                }
                else
                    return new CommonResponse<string> { RequestStatus = RequestStatus.ServerError, Message = "ServerError", ModelError = result.Errors.Select(c => string.Join(',', c.Description)) };
            }
            catch (Exception ex)
            {
                return new CommonResponse<string> { RequestStatus = RequestStatus.ServerError, Message = "ServerError" };
            }

        }
        public async Task CreateDefaultUsersAsync()
        {
            if (!await CheckEmailIsExistsAsync("Owner"))
            {
                var user = new ApplicationUserVM
                {
                    Email = "Owner@paysky.com",
                    FullName = "Owner",
                    Password = "Owner@123",
                    PhoneNumber = "01271870153",
                    RoleName = Role.SuperAdmin.ToString(),
                    UserName = "Owner"
                };
                await CreateUserAsync(user);
            }

        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var _jwtSettings = options.Value;

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        public async Task<CommonResponse<LoginUserDto>> Login(LoginVM model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);
                var _data = new LoginUserDto
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Role = userRoles.FirstOrDefault()

                };
                return new CommonResponse<LoginUserDto> { Message = "Success", RequestStatus = RequestStatus.Success, Data = _data };
            }
            return new CommonResponse<LoginUserDto> { Message = "invalid username or password", RequestStatus = RequestStatus.Unauthorized };

        }

        public async Task<ApplicationUser> GetCurrentUser()
        {
            string userid = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _userManager.FindByIdAsync(userid);
        }
        public async Task<bool> CkeckUserInRole(ApplicationUser user, string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> CheckUserNameIsExistsAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user != null;
        }

        public async Task<bool> CheckPhoneIsExistsAsync(string phone)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(c => c.PhoneNumber == phone);
            return user != null;
        }

        #endregion
    }
}
