using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.DbContexts;
using ShoppingCart.Models;

namespace ShoppingCart.EndPointsHandler
{
    public static class ImageHandler
    {
        public static async Task<Results<NotFound, Ok<IEnumerable<ImageDto>>>> GetImagesAsync(ShoppingDbContext dbContext, IMapper mapper, ILogger<ImageDto> logger, int productId)
        {
            logger.LogInformation("Adding log information");

            var imageCollection = await dbContext.products
                .Include(d => d.Images)
                .FirstOrDefaultAsync(dbContext => dbContext.Id == productId);
            if (imageCollection == null)
                return TypedResults.NotFound();
            else
            {
                if (!imageCollection.Images.Any())
                    return TypedResults.NotFound();
        
                return TypedResults.Ok(mapper.Map<IEnumerable<ImageDto>>(imageCollection.Images));
            }

        }
    }
}
