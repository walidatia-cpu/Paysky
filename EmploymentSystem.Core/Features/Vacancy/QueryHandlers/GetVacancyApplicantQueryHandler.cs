using EmploymentSystem.Core.Dto.Vacancy;
using EmploymentSystem.Core.Features.Vacancy.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Core.Features.Vacancy.QueryHandlers
{
    public class GetVacancyApplicantQueryHandler:IRequestHandler<GetVacancyApplicantQuery, List<ApplicantDto>>
    {
    }
}
