using ShoppingCart.EndPointsHandler;

namespace ShoppingCart.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void RegisterProductEndPoints (this IEndpointRouteBuilder app)
        {
            var productEndPoints = app.MapGroup("/products");
            var productWithProductIdEndPoints = productEndPoints.MapGroup("/{productId:int}");
            
            productEndPoints.MapGet("", ProductHandler.GetProductAsync);

            productWithProductIdEndPoints.MapGet("", ProductHandler.GetProductDetailAsync).WithName("GetProduct");

            productEndPoints.MapPost("", ProductHandler.AddProductAsync);

            productWithProductIdEndPoints.MapPut("", ProductHandler.UpdateProductAsync);

            productWithProductIdEndPoints.MapDelete("", ProductHandler.DeleteProductAsync);

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
