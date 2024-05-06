using AutoMapper;
using EmploymentSystem.Core.Dto.Vacancy;
using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Features.Vacancy.Commands;

namespace EmploymentSystem.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Remember
            // CreateMap<SourceClass, DestinationClass>();
            #endregion

            #region Vacancy
            _CreateVacancyCommandMap();
            _UpdateVacancyCommandMap();
            _GetAllVacanciesQueryMap();
            _CreateVacancyApplicantCommandMap();
            _SearchForVacancyQueryMap();
            #endregion
        }
        void _CreateVacancyCommandMap()
        {
            CreateMap<CreateVacancyCommand, Vacancy>();
        }
        void _UpdateVacancyCommandMap()
        {
            CreateMap<UpdateVacancyCommand, Vacancy>();
        }
        void _GetAllVacanciesQueryMap()
        {
            CreateMap<Vacancy, VacancyDto>().
                ForMember(dest=>dest.ApplicantCount,opt=>opt.MapFrom(src=>src.VacancyApplicants.Count));
        }

        void _CreateVacancyApplicantCommandMap()
        {
            CreateMap<CreateVacancyApplicantCommand, VacancyApplicant>();
        }
        void _SearchForVacancyQueryMap()
        {
            CreateMap<Vacancy, SearchForVacancyDto>();
        }
       
       

    }
}
