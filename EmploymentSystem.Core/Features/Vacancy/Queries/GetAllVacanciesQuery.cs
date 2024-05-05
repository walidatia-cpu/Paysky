using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Dto.Vacancy;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EmploymentSystem.Core.Features.Vacancy.Queries
{
    public class GetAllVacanciesQuery : IRequest<CommonResponse<List<VacancyDto>>>
    {
        [Required(ErrorMessage = "CreationDateFrom is required")]
        public DateTime CreationDateFrom { get; set; }
        [Required(ErrorMessage = "CreationDateTo is required")]
        public DateTime CreationDateTo { get; set; }
        [Required(ErrorMessage = "Page is required")]
        public int Page { get; set; } = 1;
        [Required(ErrorMessage = "PageCount is required")]
        public int PageCount { get; set; } = 1;
    }
}
