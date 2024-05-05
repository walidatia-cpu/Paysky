using EmploymentSystem.Core.Dto;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EmploymentSystem.Core.Features.Vacancy.Commands
{
    public class CreateVacancyApplicantCommand : IRequest<CommonResponse<string>>
    {
        [Required(ErrorMessage = "VacancyId is required")]
        public string VacancyId { get; set; }
        [Required(ErrorMessage = "BIO is required")]
        public string BIO { get; set; }
    }
}
