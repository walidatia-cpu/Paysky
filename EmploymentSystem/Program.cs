using EmploymentSystem.AutoMapper;
using EmploymentSystem.Extensions;
using EmploymentSystem.Filters.ActionFilter;
using FluentValidation;
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

// migrate database 
using (var scope = app.Services.CreateScope())
{
    scope.MigrateDatabase();
    await scope.SeedDefaultData();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
       .AllowAnyHeader()
       .AllowAnyMethod()
       .AllowAnyOrigin()
    );

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
