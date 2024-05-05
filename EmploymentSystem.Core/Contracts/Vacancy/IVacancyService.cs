using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Dto.Vacancy;
using EmploymentSystem.Core.Features.Vacancy.Commands;
using EmploymentSystem.Core.Features.Vacancy.Queries;

namespace EmploymentSystem.Core.Contracts.Vacancy
{
    public interface IVacancyService
    {
        Task<CommonResponse<string>> CreateVacancy(CreateVacancyCommand command);
        Task<CommonResponse<string>> UpdateVacancy(UpdateVacancyCommand command);
        Task<CommonResponse<string>> DeleteVacancy(DeleteVacancyCommand command);
        Task<CommonResponse<string>> ToggleVacancyStatus(ToggleVacancyStatusCommand command);
        Task<CommonResponse<List<VacancyDto>>> GetAllVacancies(GetAllVacanciesQuery query);
    }
}
