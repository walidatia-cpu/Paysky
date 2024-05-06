namespace EmploymentSystem.Core.Dto.Vacancy
{
    public class VacancyDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsArchived { get; set; }
        public string ExpiryDate { get; set; }
        public int VacancyMaxNumber { get; set; }
        public bool IsActive { get; set; }
        public string CreationDate { get; set; }
        public string? LastModificationDate { get; set; }
        public int ApplicantCount { get; set; }
    }
}
