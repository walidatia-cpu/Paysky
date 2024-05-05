using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Features.Vacancy.Commands;

namespace EmploymentSystem.Core.Contracts.Vacancy
{
    public interface IVacancyApplicantService
    {
        Task<CommonResponse<string>> ApplyVacancy(CreateVacancyApplicantCommand command);
    }
}
