﻿using EmploymentSystem.Core.Dto;
using EmploymentSystem.Core.Dto.Vacancy;
using EmploymentSystem.Core.Features.Vacancy.Commands;
using EmploymentSystem.Core.Features.Vacancy.Queries;
using EmploymentSystem.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.Controllers.Vacancies
{
    [Route("api/v1/Vacancies")]
    [ApiController]
    [TypeFilter(typeof(JwtAuthorizeAttribute))]
    public class VacanciesController : ControllerBase
    {
        #region fields
        private readonly IMediator mediator;
        #endregion

        #region ctor
        public VacanciesController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        #endregion

        #region methods

        #region Employer
        [HttpPost]
        [Route("CreateVacancy")]
        public async Task<CommonResponse<string>> CreateVacancy([FromBody] CreateVacancyCommand model)
        {
            return await mediator.Send(model);
        }

        [HttpPost]
        [Route("DeleteVacancy")]
        public async Task<CommonResponse<string>> DeleteVacancy([FromBody] DeleteVacancyCommand model)
        {
            return await mediator.Send(model);
        }

        [HttpPost]
        [Route("UpdateVacancy")]
        public async Task<CommonResponse<string>> UpdateVacancy([FromBody] UpdateVacancyCommand model)
        {

            return await mediator.Send(model);
        }

        [HttpPost]
        [Route("ToggleVacancyStatus")]
        public async Task<CommonResponse<string>> ToggleVacancyStatus([FromBody] ToggleVacancyStatusCommand model)
        {
            return await mediator.Send(model);
        }

        [HttpPost]
        [Route("GetAllVacancies")]
        public async Task<CommonResponse<List<VacancyDto>>> GetAllVacancies([FromBody] GetAllVacanciesQuery model)
        {
            return await mediator.Send(model);
        }

        [HttpPost]
        [Route("GetVacancyApplicant")]
        public async Task<CommonResponse<List<ApplicantDto>>> GetVacancyApplicant([FromBody] GetVacancyApplicantQuery model)
        {
            return await mediator.Send(model);
        }
        #endregion Employer

        #region Applicant

        [HttpPost]
        [Route("ApplyVacancy")]
        public async Task<CommonResponse<string>> ApplyVacancy([FromBody] CreateVacancyApplicantCommand model)
        {
            return await mediator.Send(model);
        }

        [HttpPost]
        [Route("SearchForVacancy")]
        public async Task<CommonResponse<List<SearchForVacancyDto>>> SearchForVacancy([FromBody] SearchForVacancyQuery model)
        {
            return await mediator.Send(model);
        }

        [HttpPost]
        [Route("MyVacancies")]
        public async Task<CommonResponse<List<VacancyDto>>> MyVacancies([FromBody] GetMyApplicantVacanciesQuery model)
        {
            return await mediator.Send(model);
        }
        #endregion applicant

        #endregion methods
    }
}
