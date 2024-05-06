using EmploymentSystem.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmploymentSystem.DAL.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ApplicationUser.Config(modelBuilder);
            Vacancy.Config(modelBuilder);
        }

        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<VacancyApplicant> VacancyApplicants { get; set; }

    }
}
