using EmploymentSystem.Core.Contracts.Vacancy;
using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Features.Vacancy.Commands;
using MediatR;

namespace EmploymentSystem.Core.Features.Vacancy.CommandHandlers
{
    public class CreateVacancyCommandHandler : IRequestHandler<CreateVacancyCommand, CommonResponse<string>>
    {
        private readonly IVacancyService vacancyService;

        public CreateVacancyCommandHandler(IVacancyService vacancyService)
        {
            this.vacancyService = vacancyService;
        }
        public async Task<CommonResponse<string>> Handle(CreateVacancyCommand request, CancellationToken cancellationToken)
        {
            return await vacancyService.CreateVacancy(request);
        }
    }
}
