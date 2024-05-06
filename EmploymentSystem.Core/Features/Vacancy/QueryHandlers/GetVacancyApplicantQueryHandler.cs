using EmploymentSystem.Core.Contracts.Vacancy;
using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Dto.Vacancy;
using EmploymentSystem.Core.Features.Vacancy.Queries;
using MediatR;

namespace EmploymentSystem.Core.Features.Vacancy.QueryHandlers
{
    public class GetVacancyApplicantQueryHandler : IRequestHandler<GetVacancyApplicantQuery, CommonResponse<List<ApplicantDto>>>
    {
        private readonly IVacancyApplicantService vacancyApplicantService;

        public GetVacancyApplicantQueryHandler(IVacancyApplicantService vacancyApplicantService)
        {
            this.vacancyApplicantService = vacancyApplicantService;
        }
        public async Task<CommonResponse<List<ApplicantDto>>> Handle(GetVacancyApplicantQuery request, CancellationToken cancellationToken)
        {
            return await vacancyApplicantService.GetVacancyApplicant(request);
        }
    }
}
