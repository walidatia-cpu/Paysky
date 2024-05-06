using EmploymentSystem.Core.Dto.Vacancy;
using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Features.Vacancy.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmploymentSystem.Core.Contracts.Vacancy;
using EmploymentSystem.Core.Contracts.Identity;

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
