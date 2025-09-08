using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ReservationApp.Utilities.Filters
{
    public class FluentValidationActionFilter : IAsyncActionFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public FluentValidationActionFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async  Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var arg in context.ActionArguments.Values)
            {
                if (arg == null) continue;

                var argType = arg.GetType();
                var validatorType = typeof(IValidator<>).MakeGenericType(argType);

                var validator = _serviceProvider.GetService(validatorType) as IValidator;
                if (validator == null) continue;

                var validationContext = new ValidationContext<object>(arg);
                var result = await validator.ValidateAsync(validationContext);

                if (!result.IsValid)
                {
                    var errors = result.Errors
                        .Select(e => new { e.PropertyName, e.ErrorMessage })
                        .ToList();

                    context.Result = new BadRequestObjectResult(new
                    {
                        Message = "Validation failed",
                        Errors = errors
                    });
                    return; 
                }
            }

            await next();
        }
    }
}
