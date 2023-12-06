using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.DbContexts;
using ShoppingCart.Entities;
using ShoppingCart.Models;

namespace ShoppingCart.EndPointsHandler
{
    public static class ProductHandler
    {
        public static async Task<Results<NotFound, Ok<IEnumerable<ProductDto>>>> GetProductAsync(ShoppingDbContext dbContext, IMapper mapper)
        {
            var productCollection = await dbContext.products.ToListAsync();
            if (productCollection.Any())
                return TypedResults.Ok(mapper.Map<IEnumerable<ProductDto>>(productCollection));
            else
                return TypedResults.NotFound();

        }

        public static async Task<Results<NotFound, Ok<ProductDto>>> GetProductDetailAsync(ShoppingDbContext dbContext, IMapper mapper, int productId)
        {
            var details = await dbContext.products.FirstOrDefaultAsync(d => d.Id == productId);
            if (details == null)
                return TypedResults.NotFound();

            return TypedResults.Ok(mapper.Map<ProductDto>(details));

        }

        public static async Task<CreatedAtRoute<ProductDto>> AddProductAsync(ShoppingDbContext dbContext, IMapper mapper, ProductForCreationDto product)
        {
            var productEntity = mapper.Map<Product>(product);
            dbContext.Add(productEntity);
            await dbContext.SaveChangesAsync();

            var productReturn = mapper.Map<ProductDto>(productEntity);
            return TypedResults.CreatedAtRoute(productReturn, "GetProduct", new
            {
                ProductId = productReturn.Id
            });
        }

        public static async Task<Results<NotFound, Ok>> UpdateProductAsync(ShoppingDbContext dbcontext, ProductforUpdateDto productDetails, int productId, IMapper mapper)
        {
            var productEntity = await dbcontext.products.FirstOrDefaultAsync(d => d.Id == productId);
            if (productEntity == null)
                return TypedResults.NotFound();

            mapper.Map(productDetails, productEntity);
            await dbcontext.SaveChangesAsync();

            return TypedResults.Ok();
        }

        public static async Task<Results<NotFound, Ok>> DeleteProductAsync(ShoppingDbContext dbContext, int productId)
        {
            var productEntity = await dbContext.products.FirstOrDefaultAsync(d => d.Id == productId);
            if (productEntity == null)
                return TypedResults.NotFound();

            dbContext.products.Remove(productEntity);
            await dbContext.SaveChangesAsync();

            return TypedResults.Ok();
        }
    }
}
