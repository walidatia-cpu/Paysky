using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Dto.Vacancy;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Core.Features.Vacancy.Queries
{
    public class SearchForVacancyQuery:IRequest<CommonResponse<List<SearchForVacancyDto>>>
    {
        [Required(ErrorMessage = "SearchTerm  is Required")]
        public string SearchTerm { get; set; }
    }
}
