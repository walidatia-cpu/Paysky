using EmploymentSystem.Core.Contracts.Vacancy;
using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Features.Vacancy.Commands;
using MediatR;

namespace EmploymentSystem.Core.Features.Vacancy.CommandHandlers
{
    public class CreateVacancyApplicantCommandHandler : IRequestHandler<CreateVacancyApplicantCommand, CommonResponse<string>>
    {
        private readonly IVacancyApplicantService vacancyApplicantService;

        public CreateVacancyApplicantCommandHandler(IVacancyApplicantService vacancyApplicantService)
        {
            this.vacancyApplicantService = vacancyApplicantService;
        }
        public async Task<CommonResponse<string>> Handle(CreateVacancyApplicantCommand request, CancellationToken cancellationToken)
        {
            return await vacancyApplicantService.ApplyVacancy(request);
        }
    }
}
