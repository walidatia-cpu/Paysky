using EmploymentSystem.AutoMapper;
using EmploymentSystem.Extensions;
using EmploymentSystem.Filters.ActionFilter;
using EmploymentSystem.Hangfire;
using FluentValidation;
using Hangfire;
using Hangfire.Dashboard;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Config dbcontext and Identity
builder.Services.AddConfig(builder.Configuration);
builder.Services.AddJWTAuthentication(builder.Configuration);
foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
{
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
}
//Register My Services
builder.Services.AddMyDependencyGroup();
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ValidateModelAttribute));
});
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseStaticFiles();
app.AddGlobalErrorHandler();

#region migrate database
// migrate database 
using (var scope = app.Services.CreateScope())
{
    scope.MigrateDatabase();
    await scope.SeedDefaultData();
}
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Hangfire
// Use Hangfire server and dashboard
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    // Configure Hangfire Dashboard options here
    IsReadOnlyFunc = (DashboardContext context) => false
});
app.UseHangfireServer();

// Create a scope to resolve services
using (var scope = app.Services.CreateScope())
{
    // Get an instance of your background job class from the service provider
    var backgroundJob = scope.ServiceProvider.GetService<MyBackgroundJob>();
    // Schedule the job to run daily
    RecurringJob.AddOrUpdate(() => backgroundJob.ArchivExpiredVacancies(), Cron.Hourly);
}

#endregion


app.UseCors(builder => builder
       .AllowAnyHeader()
       .AllowAnyMethod()
       .AllowAnyOrigin()
    );

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
