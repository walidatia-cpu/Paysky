using EmploymentSystem.Core.Dto;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EmploymentSystem.Core.Features.Vacancy.Commands
{
    public class UpdateVacancyCommand : IRequest<CommonResponse<string>>
    {
        [Required(ErrorMessage = "VacancyId is required")]
        public string VacancyId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "ExpiryDate is required")]
        public DateOnly ExpiryDate { get; set; }
        [Required(ErrorMessage = "VacancyMaxNumber is required")]
        public int VacancyMaxNumber { get; set; }
    }
}
