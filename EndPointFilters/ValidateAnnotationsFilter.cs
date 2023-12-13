using MiniValidation;
using ShoppingCart.Models;

namespace ShoppingCart.EndPointFilters
{
    public class ValidateAnnotationsFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var productForCreationDto = context.GetArgument<ProductForCreationDto>(2);

            if(!MiniValidator.TryValidate(productForCreationDto, out var errors))
            {
                return TypedResults.ValidationProblem(errors);
            }

            return await next(context);
        }
    }
}
