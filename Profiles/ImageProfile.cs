using AutoMapper;
using ShoppingCart.Entities;
using ShoppingCart.Models;

namespace ShoppingCart.Profiles
{
    public class ImageProfile: Profile
    {
        public ImageProfile()
        {
            CreateMap<Image, ImageDto>();
        }
    }
}
