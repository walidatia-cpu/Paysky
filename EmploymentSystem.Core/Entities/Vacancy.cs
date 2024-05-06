using Microsoft.EntityFrameworkCore;

namespace EmploymentSystem.Core.Entities
{
    public class Vacancy : BaseEntity
    {
        public Vacancy()
        {
            this.VacancyApplicants = new List<VacancyApplicant>();
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsArchived { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int VacancyMaxNumber { get; set; }
        public virtual List<VacancyApplicant> VacancyApplicants { get; set; }

        public static void Config(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Vacancy>()
                .HasIndex(u => u.Title);

            modelBuilder.Entity<Vacancy>()
                .HasIndex(u => u.Description);
        }
    }
}
