using AutoMapper;
using ShoppingCart.Entities;
using ShoppingCart.Models;

namespace ShoppingCart.Profiles
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductForCreationDto, Product>();
            CreateMap<ProductforUpdateDto, Product>();
        }
    }
}
