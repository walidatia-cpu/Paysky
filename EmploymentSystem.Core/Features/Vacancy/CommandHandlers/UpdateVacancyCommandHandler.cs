using EmploymentSystem.Core.Contracts.Vacancy;
using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Features.Vacancy.Commands;
using MediatR;

namespace EmploymentSystem.Core.Features.Vacancy.CommandHandlers
{
    public class UpdateVacancyCommandHandler : IRequestHandler<UpdateVacancyCommand, CommonResponse<string>>
    {
        private readonly IVacancyService vacancyService;

        public UpdateVacancyCommandHandler(IVacancyService vacancyService)
        {
            this.vacancyService = vacancyService;
        }
        public async Task<CommonResponse<string>> Handle(UpdateVacancyCommand request, CancellationToken cancellationToken)
        {
            return await vacancyService.UpdateVacancy(request);
        }
    }
}
