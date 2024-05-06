using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Dto.Vacancy;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EmploymentSystem.Core.Features.Vacancy.Queries
{
    public class SearchForVacancyQuery : IRequest<CommonResponse<List<SearchForVacancyDto>>>
    {
        [Required(ErrorMessage = "SearchTerm  is Required")]
        public string SearchTerm { get; set; }
    }
}
