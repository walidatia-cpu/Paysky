using EmploymentSystem.Core.Contracts.Vacancy;
using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Dto.Vacancy;
using EmploymentSystem.Core.Features.Vacancy.Queries;
using MediatR;

namespace EmploymentSystem.Core.Features.Vacancy.QueryHandlers
{
    public class GetAllVacanciesQueryHandler : IRequestHandler<GetAllVacanciesQuery, CommonResponse<List<VacancyDto>>>
    {
        private readonly IVacancyService vacancyService;


        public GetAllVacanciesQueryHandler(IVacancyService vacancyService)
        {
            this.vacancyService = vacancyService;
        }
        public async Task<CommonResponse<List<VacancyDto>>> Handle(GetAllVacanciesQuery request, CancellationToken cancellationToken)
        {
            return await vacancyService.GetAllVacancies(request);
        }
    }
}
