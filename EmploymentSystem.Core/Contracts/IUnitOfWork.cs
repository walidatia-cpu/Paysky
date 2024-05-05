namespace EmploymentSystem.Core.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync();
    }
}
