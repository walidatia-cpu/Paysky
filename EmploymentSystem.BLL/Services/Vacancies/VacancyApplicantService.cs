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

                // check if Applicant is  apply for more than one vacancy per day (24 hours)
                var _currentUser = await userService.GetCurrentUser();
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
        #endregion
    }
}
