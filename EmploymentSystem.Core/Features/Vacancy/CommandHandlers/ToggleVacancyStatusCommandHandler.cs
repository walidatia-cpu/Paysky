using EmploymentSystem.Core.Contracts.Vacancy;
using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Features.Vacancy.Commands;
using MediatR;

namespace EmploymentSystem.Core.Features.Vacancy.CommandHandlers
{
    public class ToggleVacancyStatusCommandHandler : IRequestHandler<ToggleVacancyStatusCommand, CommonResponse<string>>
    {
        private readonly IVacancyService vacancyService;

        public ToggleVacancyStatusCommandHandler(IVacancyService vacancyService)
        {
            this.vacancyService = vacancyService;
        }
        public async Task<CommonResponse<string>> Handle(ToggleVacancyStatusCommand request, CancellationToken cancellationToken)
        {
            return await vacancyService.ToggleVacancyStatus(request);
        }
    }
}
