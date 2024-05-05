﻿using EmploymentSystem.Core.Dto;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EmploymentSystem.Core.Features.Vacancy.Commands
{
    public class DeleteVacancyCommand : IRequest<CommonResponse<string>>
    {
        [Required(ErrorMessage = "VacancyId is required")]
        public string VacancyId { get; set; }
    }
}
