using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Dto.Vacancy;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EmploymentSystem.Core.Features.Vacancy.Queries
{
    public class GetVacancyApplicantQuery : IRequest<CommonResponse<List<ApplicantDto>>>
    {
        [Required(ErrorMessage = "VacancyId is required")]
        public string VacancyId { get; set; }
    }
}
