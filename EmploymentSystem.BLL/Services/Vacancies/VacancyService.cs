namespace EmploymentSystem.BLL.Services.Vacancies
{
    public class VacancyService : IVacancyService
    {

        #region fields

        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IAsyncRepository<Vacancy> repository;
        private readonly IUserService userService;

        #endregion

        #region ctor
        public VacancyService(IMapper mapper, IUnitOfWork unitOfWork, IAsyncRepository<Vacancy> repository, IUserService userService)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.userService = userService;
        }
        #endregion

        #region methods

        public async Task<CommonResponse<string>> CreateVacancy(CreateVacancyCommand command)
        {
            try
            {
                #region validation
                // check user if is Employer
                var _currentUser = await userService.GetCurrentUser();
                if (!await userService.CkeckUserInRole(_currentUser, Role.Employer.ToString()))
                    return new CommonResponse<string> { RequestStatus = RequestStatus.BadRequest, Message = "sorry, You are not allowed to create vacancy" };
                #endregion
                #region Create
                var _vacancy = mapper.Map<Vacancy>(command);
                _vacancy.CreationDate = DateTime.Now;
                _vacancy.CreationBy = _currentUser.Id;
                _vacancy.IsActive = true;
                await repository.AddAsync(_vacancy);
                #endregion

                #region save changes
                await unitOfWork.SaveChangesAsync();
                #endregion

                return new CommonResponse<string> { RequestStatus = RequestStatus.Success, Message = "Success", Data = _vacancy.Id.ToString() };

            }
            catch (Exception)
            {
                return new CommonResponse<string> { RequestStatus = RequestStatus.ServerError, Message = "ServerError" };
            }

        }

        public async Task<CommonResponse<string>> DeleteVacancy(DeleteVacancyCommand command)
        {
            try
            {
                #region validation

                var _vacancy = await repository.FirstOrDefaultAsync(c => c.Id.ToString() == command.VacancyId);
                if (_vacancy == null)
                    return new CommonResponse<string> { RequestStatus = RequestStatus.BadRequest, Message = "The vacancy ID is invalid" };
                // check creation user
                var _currentUser = await userService.GetCurrentUser();
                if (_vacancy.CreationBy != _currentUser.Id)
                    return new CommonResponse<string> { RequestStatus = RequestStatus.BadRequest, Message = "You do not have permission to delete vacancy as you are not the one who created it" };

                #endregion

                #region Delete

                _vacancy.LastModificationDate = DateTime.Now;
                _vacancy.LastModificationBy = _currentUser.Id;
                _vacancy.IsActive = false;
                _vacancy.IsDeleted = true;
                await repository.UpdateAsync(_vacancy);
                #endregion

                #region save changes
                await unitOfWork.SaveChangesAsync();
                #endregion

                return new CommonResponse<string> { RequestStatus = RequestStatus.Success, Message = "Success", Data = _vacancy.Id.ToString() };

            }
            catch (Exception)
            {
                return new CommonResponse<string> { RequestStatus = RequestStatus.ServerError, Message = "ServerError" };
            }
        }

        public async Task<CommonResponse<string>> ToggleVacancyStatus(ToggleVacancyStatusCommand command)
        {
            try
            {
                #region validation
                var _vacancy = await repository.FirstOrDefaultAsync(c => c.Id.ToString() == command.VacancyId);
                if (_vacancy == null)
                    return new CommonResponse<string> { RequestStatus = RequestStatus.BadRequest, Message = "The vacancy ID is invalid" };
                // check creation user
                var _currentUser = await userService.GetCurrentUser();
                if (_vacancy.CreationBy != _currentUser.Id)
                    return new CommonResponse<string> { RequestStatus = RequestStatus.BadRequest, Message = "You do not have permission to delete vacancy as you are not the one who created it" };

                #endregion

                #region toggle status 

                _vacancy.LastModificationDate = DateTime.Now;
                _vacancy.LastModificationBy = _currentUser.Id;
                _vacancy.IsActive = _vacancy.IsActive ? false : true;
                await repository.UpdateAsync(_vacancy);
                #endregion

                #region save changes
                await unitOfWork.SaveChangesAsync();
                #endregion

                return new CommonResponse<string> { RequestStatus = RequestStatus.Success, Message = "Success", Data = _vacancy.Id.ToString() };

            }
            catch (Exception)
            {
                return new CommonResponse<string> { RequestStatus = RequestStatus.ServerError, Message = "ServerError" };
            }
        }

        public async Task<CommonResponse<string>> UpdateVacancy(UpdateVacancyCommand command)
        {
            try
            {
                #region validation
                var _vacancy = await repository.FirstOrDefaultAsync(c => c.Id.ToString() == command.VacancyId);
                if (_vacancy == null)
                    return new CommonResponse<string> { RequestStatus = RequestStatus.BadRequest, Message = "The vacancy ID is invalid" };
                // check creation user
                var _currentUser = await userService.GetCurrentUser();
                if (_vacancy.CreationBy != _currentUser.Id)
                    return new CommonResponse<string> { RequestStatus = RequestStatus.BadRequest, Message = "You do not have permission to delete vacancy as you are not the one who created it" };

                #endregion

                #region update
                _vacancy = mapper.Map(command, _vacancy);
                _vacancy.LastModificationDate = DateTime.Now;
                _vacancy.LastModificationBy = _currentUser.Id;
                await repository.UpdateAsync(_vacancy);
                #endregion

                #region save changes
                await unitOfWork.SaveChangesAsync();
                #endregion

                return new CommonResponse<string> { RequestStatus = RequestStatus.Success, Message = "Success", Data = _vacancy.Id.ToString() };

            }
            catch (Exception)
            {
                return new CommonResponse<string> { RequestStatus = RequestStatus.ServerError, Message = "ServerError" };
            }
        }


        public async Task<CommonResponse<List<VacancyDto>>> GetAllVacancies(GetAllVacanciesQuery query)
        {
            try
            {
                var _currentUser = await userService.GetCurrentUser();

                var _Vacancies = await repository.GetAllAsync(c => c.CreationBy == _currentUser.Id
                && c.CreationDate.Date >= query.CreationDateFrom.Date
                && c.CreationDate.Date <= query.CreationDateTo.Date
                && c.IsDeleted != true
                , query.Page, query.PageCount);

                return new CommonResponse<List<VacancyDto>> { RequestStatus = RequestStatus.Success, Message = "Success", Data = mapper.Map<List<VacancyDto>>(_Vacancies) };

            }
            catch (Exception)
            {
                return new CommonResponse<List<VacancyDto>> { RequestStatus = RequestStatus.ServerError, Message = "ServerError" };
            }
        }

        public async Task<CommonResponse<List<SearchForVacancyDto>>> SearchForVacancy(SearchForVacancyQuery query)
        {
            try
            {
                #region validation
                // check user if is Applicant
                var _currentUser = await userService.GetCurrentUser();
                if (!await userService.CkeckUserInRole(_currentUser, Role.Applicant.ToString()))
                    return new CommonResponse<List<SearchForVacancyDto>> { RequestStatus = RequestStatus.BadRequest, Message = "sorry, You are not allowed to search for vacancy" };
                #endregion
                var _Vacancies = await repository.GetAllAsync(c => (c.Title.Contains(query.SearchTerm) || c.Description.Contains(query.SearchTerm)) && c.IsDeleted != true && c.IsArchived != true);

                return new CommonResponse<List<SearchForVacancyDto>> { RequestStatus = RequestStatus.Success, Message = "Success", Data = mapper.Map<List<SearchForVacancyDto>>(_Vacancies) };

            }
            catch (Exception)
            {
                return new CommonResponse<List<SearchForVacancyDto>> { RequestStatus = RequestStatus.ServerError, Message = "ServerError" };
            }
        }

        public async Task ArchivExpiredVacancies()
        {
            try
            {

                var _Vacancies = await repository.GetAllAsync(c => c.ExpiryDate <= DateTime.Now && c.IsArchived != true);
                _Vacancies.ToList().ForEach(v => { v.IsArchived = true; });
                await unitOfWork.SaveChangesAsync();

            }
            catch (Exception)
            {

            }
        }




        #endregion
    }
}
