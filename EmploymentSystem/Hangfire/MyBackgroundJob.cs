using EmploymentSystem.Core.Contracts.Vacancy;

namespace EmploymentSystem.Hangfire
{
    public class MyBackgroundJob
    {
        private readonly IServiceProvider _serviceProvider;

        public MyBackgroundJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ArchivExpiredVacancies()
        {
            // Create a scope to resolve scoped or transient services
            using (var scope = _serviceProvider.CreateScope())
            {
                // Resolve the service
                var myService = scope.ServiceProvider.GetRequiredService<IVacancyService>();
                // Use the service
                myService.ArchivExpiredVacancies().Wait();
            }
        }

    }
}
