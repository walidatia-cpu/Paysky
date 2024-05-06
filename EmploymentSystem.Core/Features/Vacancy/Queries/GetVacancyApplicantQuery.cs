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
    public class GetVacancyApplicantQuery:IRequest<List<ApplicantDto>>
    {
        [Required(ErrorMessage = "VacancyId is required")]
        public string VacancyId { get; set; }
    }
}
