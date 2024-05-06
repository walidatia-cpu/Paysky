using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Core.Dto.Vacancy
{
    public class SearchForVacancyDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ExpiryDate { get; set; }
        public int VacancyMaxNumber { get; set; }
        public string CreationDate { get; set; }
    }
}
