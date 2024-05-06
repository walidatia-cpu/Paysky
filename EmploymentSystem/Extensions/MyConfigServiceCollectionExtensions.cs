using EmploymentSystem.BLL.Services;
using EmploymentSystem.BLL.Services.Identity;
using EmploymentSystem.BLL.Services.Vacancies;
using EmploymentSystem.Core.Contracts;
using EmploymentSystem.Core.Contracts.Identity;
using EmploymentSystem.Core.Contracts.Vacancy;
using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.JWT;
using EmploymentSystem.DAL.Data;
using EmploymentSystem.Hangfire;
using EmploymentSystem.Security;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace EmploymentSystem.Extensions
{
    public static class MyConfigServiceCollectionExtensions
    {
        public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // Configure identity options if needed
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddHangfire(configx =>
               configx.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                     .UseSimpleAssemblyNameTypeSerializer()
                     .UseRecommendedSerializerSettings()
                     .UseSqlServerStorage(config.GetConnectionString("HangfireConnection")));

            return services;
        }
        public static IServiceCollection AddMyDependencyGroup(this IServiceCollection services)
        {
            #region Identity
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            #endregion

            #region Repository
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(Repository<>));
            #endregion

            #region Security
            services.AddScoped<JwtAuthorizeAttribute>();
            #endregion

            #region Vacancy
            services.AddScoped<IVacancyService, VacancyService>();
            services.AddScoped<IVacancyApplicantService, VacancyApplicantService>();
            #endregion

            #region BackgroundJob
            services.AddSingleton<MyBackgroundJob>();
            #endregion

            return services;
        }
        public static IServiceScope MigrateDatabase(this IServiceScope app)
        {
            #region applay migration
            var dataContext = app.ServiceProvider.GetRequiredService<AppDbContext>();
            dataContext.Database.Migrate();
            #endregion
            return app;
        }
        public async static Task<IServiceScope> SeedDefaultData(this IServiceScope app)
        {
            #region Seed Default Data
            var roleService = app.ServiceProvider.GetRequiredService<IRoleService>();
            var userService = app.ServiceProvider.GetRequiredService<IUserService>();
            await roleService.CreateDefaultRolesAsync();
            await userService.CreateDefaultUsersAsync();
            #endregion
            return app;
        }
        public static IServiceCollection AddJWTAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var appSettingsSection = config.GetSection("JwtSettings");
            services.Configure<JWTSettings>(appSettingsSection);

            var jwtSettings = config.GetSection("JwtSettings").Get<JWTSettings>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options =>
                        {

                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidIssuer = jwtSettings.Issuer,
                                ValidAudience = jwtSettings.Audience
                            };

                        });
            return services;
        }
    }
}
