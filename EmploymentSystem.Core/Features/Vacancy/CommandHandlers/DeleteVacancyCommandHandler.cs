using EmploymentSystem.Core.Contracts.Vacancy;
using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Features.Vacancy.Commands;
using MediatR;

namespace EmploymentSystem.Core.Features.Vacancy.CommandHandlers
{
    public class DeleteVacancyCommandHandler : IRequestHandler<DeleteVacancyCommand, CommonResponse<string>>
    {
        private readonly IVacancyService vacancyService;

        public DeleteVacancyCommandHandler(IVacancyService vacancyService)
        {
            this.vacancyService = vacancyService;
        }
        public async Task<CommonResponse<string>> Handle(DeleteVacancyCommand request, CancellationToken cancellationToken)
        {
            return await vacancyService.DeleteVacancy(request);
        }
    }
}
