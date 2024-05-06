using EmploymentSystem.Core.Contracts.Vacancy;
using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Dto.Vacancy;
using EmploymentSystem.Core.Features.Vacancy.Queries;
using MediatR;

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
