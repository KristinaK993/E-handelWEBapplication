using AutoMapper;
using E_handelWEBapplication.Models;   
using E_handelWEBapplication.DTOs;     

namespace E_handelWEBapplication.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Review, ReviewDto>().ReverseMap();
        }
    }
}
