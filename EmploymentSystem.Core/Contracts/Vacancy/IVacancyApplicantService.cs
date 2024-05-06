using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Dto.Vacancy;
using EmploymentSystem.Core.Features.Vacancy.Commands;
using EmploymentSystem.Core.Features.Vacancy.Queries;

namespace EmploymentSystem.Core.Contracts.Vacancy
{
    public interface IVacancyApplicantService
    {
        Task<CommonResponse<string>> ApplyVacancy(CreateVacancyApplicantCommand command);
        Task<CommonResponse<List<VacancyDto>>> GetMyApplicantVacancies(GetMyApplicantVacanciesQuery query);
        Task<CommonResponse<List<ApplicantDto>>> GetVacancyApplicant(GetVacancyApplicantQuery query);
    }
}
