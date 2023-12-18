using ShoppingCart.EndPointFilters;
using ShoppingCart.EndPointsHandler;

namespace ShoppingCart.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void RegisterProductEndPoints (this IEndpointRouteBuilder app)
        {
            var productEndPoints = app.MapGroup("/products")
                .RequireAuthorization("RequireUserFromIndia");
            var productWithProductIdEndPoints = productEndPoints.MapGroup("/{productId:int}");
            var productWithProductIdEndPointsWithFilters = productWithProductIdEndPoints.MapGroup("")
                .AddEndpointFilter(new ProductLockedFilter(1))
                .AddEndpointFilter(new ProductLockedFilter(2));

            productEndPoints.MapGet("", ProductHandler.GetProductAsync);

            productWithProductIdEndPoints.MapGet("", ProductHandler.GetProductDetailAsync)
                .WithName("GetProduct")
                .WithOpenApi()
                .WithSummary("Get Product based on Id")
                .WithDescription("Get Product based on Id");

            productEndPoints.MapPost("", ProductHandler.AddProductAsync)
                .AddEndpointFilter<ValidateAnnotationsFilter>()
                .ProducesValidationProblem(400);

            productWithProductIdEndPointsWithFilters.MapPut("", ProductHandler.UpdateProductAsync);

            productWithProductIdEndPointsWithFilters.MapDelete("", ProductHandler.DeleteProductAsync)
                .AddEndpointFilter<LogNotFoundResponseFilter>()
                .ProducesValidationProblem(400);

        }

        public static void RegisterImageEndPoints (this IEndpointRouteBuilder app) 
        {
            var imagesEndPoint = app.MapGroup("/products/{productId:int}/images");

            //imagesEndPoint.MapGet("", ImageHandler.GetImagesAsync);
            imagesEndPoint.MapGet("", () =>
            {
                throw new NotImplementedException();
            });
        }
    }
}
