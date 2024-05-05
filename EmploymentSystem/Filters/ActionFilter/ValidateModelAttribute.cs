using EmploymentSystem.Core.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EmploymentSystem.Filters.ActionFilter
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                // Model state is not valid, return a response indicating the validation errors
                var _resutl = new CommonResponse<object>
                {
                    RequestStatus = Core.Constant.RequestStatus.BadRequest,
                    Message = "BadRequest",
                    ModelError = context.ModelState.Values.SelectMany(v => v.Errors).Select(c => c.ErrorMessage).ToList()
                };
                context.Result = new BadRequestObjectResult(_resutl);
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }
    }
}
