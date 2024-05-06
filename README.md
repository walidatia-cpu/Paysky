Step 1: Download and install .NET 8 from the link below: 
[https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-8.0.4-windows-x64-installer]
--------------------------------
Step 2: Open the link below and click the "Run in Postman" button:
 [https://documenter.getpostman.com/view/24816377/2sA3JGgjou]
-------------------------------
Step 3: Get all user types (Employer or Applicant) from the endpoint below:
{{base_url}}/api/v1/Role/GetAll
-------------------------------
Step 4: Register two types of users from the endpoint below by passing the role ID from step 3:
{{base_url}}/api/v1/Registration/CreateUser
-------------------------------
Step 5: After registering from step 4, you must log in to use other endpoints from the endpoint below:
{{base_url}}/api/v1/Auth/login
------------------------------
Step 6: After logging in, the token is automatically set in the global variable in the collection. You can see this code by selecting the scripts tab in the login endpoint.
-----------------------------
Step 7: Employers can use all endpoints of the Employer folder that exist in the Vacancies folder:
----------------------------
- Create a new vacancy: {{base_url}}/api/v1/Vacancies/CreateVacancy
- Update vacancy by vacancy ID: {{base_url}}/api/v1/Vacancies/UpdateVacancy
- Delete vacancy by vacancy ID: {{base_url}}/api/v1/Vacancies/DeleteVacancy
- Activate and deactivate vacancy: {{base_url}}/api/v1/Vacancies/ToggleVacancyStatus
- Show all vacancies: {{base_url}}/api/v1/Vacancies/GetAllVacancies
- Show all applicants of a vacancy: {{base_url}}/api/v1/Vacancies/GetVacancyApplicant
------------------------------
Step 8: Applicants can use all endpoints of the Applicant folder that exist in the Vacancies folder:
------------------------------
- Search for new vacancies: {{base_url}}/api/v1/Vacancies/SearchForVacancy
- Apply to new vacancy: {{base_url}}/api/v1/Vacancies/ApplyVacancy
- Show old vacancies: {{base_url}}/api/v1/Vacancies/MyVacancies
------------------------------
Notes:
- {{base_url}} is a global variable in the collection.
- The password in creating a user must be complex.
- Searching for new vacancies is based on title or description, and a non-cluster index is created on two columns to increase performance.
- Paging is used in /api/v1/Vacancies/GetAllVacancies and /api/v1/Vacancies/MyVacancies.
- The lock keyword is used to handle two requests simultaneously when an applicant applies to a vacancy to check the maximum number of vacancies.
- Hangfire is used to handle expiring vacancies automatically, running every hour.
- JWT is used to secure the API.
- Validation attribute is created to validate view models before action execution.
- Middleware is created for unhandled exceptions.
-------------------------------
Technologies used in this application:
- Onion Architecture
- CQRS Pattern with MediatR
- Repositories and the Unit of Work design pattern
- AutoMapper
- Hangfire
- ASP.NET Core Identity
- JWT
-------------------------------
Technologies planned to be used in this application:
- Logging (Serilog)
- Localization
- HTML Sanitization to Prevent Cross-Site Scripting Attacks
