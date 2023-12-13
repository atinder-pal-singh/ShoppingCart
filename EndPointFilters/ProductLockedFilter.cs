namespace ShoppingCart.EndPointFilters
{
    public class ProductLockedFilter : IEndpointFilter
    {
        private readonly int _tShirtId;
        public ProductLockedFilter(int tShirtId)
        {
            _tShirtId = tShirtId;
        }
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var productId = 0;
            if (context.HttpContext.Request.Method == "PUT")
            {
                productId = context.GetArgument<int>(2);
            }
            else if(context.HttpContext.Request.Method == "DELETE")
            {
                productId = context.GetArgument<int>(1);
            }
            else
            {
                throw new NotSupportedException("This filter is not supported for this scenario");
            }

            if (productId == _tShirtId)
            {
                return TypedResults.Problem(new()
                {
                    Status = 400,
                    Title = "Product is perfect and cannot be changed",
                    Detail = "You cannot update perfection"
                });
            }

            //invoke the next filter
            var result = await next(context);
            return result;
        }
    }
}
