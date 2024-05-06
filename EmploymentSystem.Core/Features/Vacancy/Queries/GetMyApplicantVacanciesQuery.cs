using EmploymentSystem.Core.Dto.Vacancy;
using EmploymentSystem.Core.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Core.Features.Vacancy.Queries
{
    public class GetMyApplicantVacanciesQuery : IRequest<CommonResponse<List<VacancyDto>>>
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
