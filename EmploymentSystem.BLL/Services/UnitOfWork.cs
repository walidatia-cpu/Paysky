namespace EmploymentSystem.BLL.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        #region  fields
        private readonly AppDbContext _context;
        #endregion

        #region ctor
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        #region methods
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        #endregion
    }
}
