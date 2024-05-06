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

namespace EmploymentSystem.Core.Features.Vacancy.QueryHandlers
{
    public class SearchForVacancyQueryHandler : IRequestHandler<SearchForVacancyQuery, CommonResponse<List<SearchForVacancyDto>>>
    {
        private readonly IVacancyService vacancyService;

        public SearchForVacancyQueryHandler(IVacancyService vacancyService)
        {
            this.vacancyService = vacancyService;
        }
        public async Task<CommonResponse<List<SearchForVacancyDto>>> Handle(SearchForVacancyQuery request, CancellationToken cancellationToken)
        {
            return await vacancyService.SearchForVacancy(request);
        }
    }
}
