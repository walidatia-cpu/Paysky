using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmploymentSystem.Core.Entities
{
    public class VacancyApplicant
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Vacancy))]
        public Guid VacancyId { get; set; }

        public virtual Vacancy Vacancy { get; set; }

        [ForeignKey(nameof(ApplicantUser))]
        public string ApplicantId { get; set; }

        public virtual ApplicationUser ApplicantUser { get; set; }

        public DateTime CreationDate { get; set; }
        public string BIO { get; set; }

    }
}
