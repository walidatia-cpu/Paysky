using EmploymentSystem.Core.Contracts.Vacancy;
using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Dto.Vacancy;
using EmploymentSystem.Core.Features.Vacancy.Queries;
using MediatR;

namespace EmploymentSystem.Core.Features.Vacancy.QueryHandlers
{
    public class GetMyApplicantVacanciesQueryHandler : IRequestHandler<GetMyApplicantVacanciesQuery, CommonResponse<List<VacancyDto>>>
    {
        private readonly IVacancyApplicantService vacancyApplicantService;

        public GetMyApplicantVacanciesQueryHandler(IVacancyApplicantService vacancyApplicantService)
        {
            this.vacancyApplicantService = vacancyApplicantService;
        }
        public async Task<CommonResponse<List<VacancyDto>>> Handle(GetMyApplicantVacanciesQuery request, CancellationToken cancellationToken)
        {
            return await vacancyApplicantService.GetMyApplicantVacancies(request);
        }
    }
}
