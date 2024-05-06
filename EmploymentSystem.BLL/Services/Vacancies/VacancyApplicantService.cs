
using EmploymentSystem.Core.Entities;

namespace EmploymentSystem.BLL.Services.Vacancies
{
    public class VacancyApplicantService : IVacancyApplicantService
    {
        #region fields
        private readonly object _object = new object();
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IAsyncRepository<VacancyApplicant> repository;
        private readonly IUserService userService;
        private readonly IAsyncRepository<Vacancy> vacancyRepo;

        #endregion

        #region ctor
        public VacancyApplicantService(IMapper mapper, IUnitOfWork unitOfWork, IAsyncRepository<VacancyApplicant> repository, IUserService userService, IAsyncRepository<Vacancy> vacancyRepo)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.userService = userService;
            this.vacancyRepo = vacancyRepo;
        }
        #endregion

        #region methods
        public async Task<CommonResponse<string>> ApplyVacancy(CreateVacancyApplicantCommand command)
        {
            try
            {
                #region validation
                //check  vacancy Is Exists
                var _vacancy = await vacancyRepo.FirstOrDefaultAsync(c => c.Id.ToString() == command.VacancyId);
                if (_vacancy is null)
                    return new CommonResponse<string> { RequestStatus = RequestStatus.BadRequest, Message = "The vacancy ID is invalid" };
                //check  vacancy status
                if (_vacancy.IsActive == false || _vacancy.IsDeleted == true || _vacancy.IsArchived == true)
                    return new CommonResponse<string> { RequestStatus = RequestStatus.BadRequest, Message = " The vacancy is not available" };

                var _currentUser = await userService.GetCurrentUser();
                // check  if Applicant is  applied for  this vacancy before
                if (await repository.FirstOrDefaultAsync(c => c.ApplicantId == _currentUser.Id && c.VacancyId.ToString() == command.VacancyId) != null)
                    return new CommonResponse<string> { RequestStatus = RequestStatus.BadRequest, Message = "sorry, you have applied for this vacancy before" };

                // check if Applicant is  apply for more than one vacancy per day (24 hours)

                var lastVacancyApplicant = (await repository.GetAllAsync(c => c.ApplicantId == _currentUser.Id)).OrderByDescending(c => c.CreationDate).FirstOrDefault();
                if (lastVacancyApplicant is not null && DateTime.Now.AddHours(-24) < lastVacancyApplicant.CreationDate)
                    return new CommonResponse<string> { RequestStatus = RequestStatus.BadRequest, Message = "sorry, Not allowed to apply for more than one vacancy per day (24 hours)" };
                // check user if is 
                if (!await userService.CkeckUserInRole(_currentUser, Role.Applicant.ToString()))
                    return new CommonResponse<string> { RequestStatus = RequestStatus.BadRequest, Message = "sorry, You are not allowed to apply vacancy" };

                string vacancyApplicantId = string.Empty;
                #endregion

                lock (_object)
                {
                    //maximum number of vacancy  applications
                    if (_vacancy.VacancyApplicants.Count >= _vacancy.VacancyMaxNumber)
                        return new CommonResponse<string> { RequestStatus = RequestStatus.BadRequest, Message = "sorry, The maximum number of vacancies has been completed" };

                    #region Create
                    var _vacancyApplicant = mapper.Map<VacancyApplicant>(command);
                    _vacancyApplicant.CreationDate = DateTime.Now;
                    _vacancyApplicant.ApplicantId = _currentUser.Id;
                    (repository.AddAsync(_vacancyApplicant)).Wait();
                    #endregion

                    #region save changes
                    unitOfWork.SaveChangesAsync().Wait();
                    vacancyApplicantId = _vacancyApplicant.Id.ToString();
                    #endregion
                }

                return new CommonResponse<string> { RequestStatus = RequestStatus.Success, Message = "Success", Data = vacancyApplicantId };

            }
            catch (Exception)
            {
                return new CommonResponse<string> { RequestStatus = RequestStatus.ServerError, Message = "ServerError" };
            }
        }

        public async Task<CommonResponse<List<VacancyDto>>> GetMyApplicantVacancies(GetMyApplicantVacanciesQuery query)
        {
            try
            {
                var _currentUser = await userService.GetCurrentUser();
                var _vacancies = await repository.GetAllAsync(c => c.ApplicantId == _currentUser.Id
                                                              && c.CreationDate.Date >= query.CreationDateFrom.Date
                                                              && c.CreationDate.Date <= query.CreationDateTo.Date
                                                              && c.Vacancy.IsDeleted != true
                                                              && c.Vacancy.IsArchived != true
                                                              , query.Page, query.PageCount);
                var _result = _vacancies.Select(c => new VacancyDto
                {
                    Description = c.Vacancy.Description,
                    CreationDate = c.CreationDate.ToString(),
                    IsArchived = c.Vacancy.IsArchived,
                    ExpiryDate = c.Vacancy.ExpiryDate.ToString(),
                    IsActive = c.Vacancy.IsActive,
                    LastModificationDate = c.CreationDate.ToString(),
                    Title = c.Vacancy.Title,
                    VacancyMaxNumber = c.Vacancy.VacancyMaxNumber,
                    Id = c.VacancyId

                }).ToList();
                return new CommonResponse<List<VacancyDto>> { RequestStatus = RequestStatus.Success, Message = "Success", Data = _result };
            }
            catch (Exception)
            {
                return new CommonResponse<List<VacancyDto>> { RequestStatus = RequestStatus.ServerError, Message = "ServerError" };

            }
        }

        public async Task<CommonResponse<List<ApplicantDto>>> GetVacancyApplicant(GetVacancyApplicantQuery query)
        {
            try
            {
                #region validation
                // check user if is Employer
                var _currentUser = await userService.GetCurrentUser();
                if (!await userService.CkeckUserInRole(_currentUser, Role.Employer.ToString()))
                    return new CommonResponse<List<ApplicantDto>> { RequestStatus = RequestStatus.BadRequest, Message = "sorry, You are not allowed to view Applicant vacancy" };
                var _vacancy = await vacancyRepo.FirstOrDefaultAsync(c => c.Id.ToString() == query.VacancyId);
                if (_vacancy == null)
                    return new CommonResponse<List<ApplicantDto>> { RequestStatus = RequestStatus.BadRequest, Message = "The vacancy ID is invalid" };
                if (_vacancy.CreationBy != _currentUser.Id)
                    return new CommonResponse<List<ApplicantDto>> { RequestStatus = RequestStatus.BadRequest, Message = "You do not have permission to view applicat vacancy as you are not the one who created it" };

                var vacancyApplicants = await repository.GetAllAsync(c => c.VacancyId.ToString() == query.VacancyId);

                var _result = vacancyApplicants.Select(c => new ApplicantDto
                {
                    BIO = c.BIO,
                    CreationDate = c.CreationDate.ToString(),
                    Email = c.ApplicantUser.Email,
                    FullName = c.ApplicantUser.FullName,
                    PhoneNumber = c.ApplicantUser.PhoneNumber

                }).ToList();
                return new CommonResponse<List<ApplicantDto>> { RequestStatus = RequestStatus.Success, Message = "Success",Data=_result };

                #endregion
            }
            catch (Exception)
            {

                return new CommonResponse<List<ApplicantDto>> { RequestStatus = RequestStatus.ServerError, Message = "ServerError" };

            }
        }
        #endregion
    }
}
