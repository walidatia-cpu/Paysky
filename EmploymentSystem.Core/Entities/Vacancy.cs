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
        public DateOnly ExpiryDate { get; set; }
        public int VacancyMaxNumber { get; set; }
        public virtual List<VacancyApplicant> VacancyApplicants { get; set; }
    }
}
