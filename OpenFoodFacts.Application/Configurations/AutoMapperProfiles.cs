using AutoMapper;
using OpenFoodFacts.Application.Product.ViewModels;

namespace OpenFoodFacts.Application.Configurations
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ProductViewModel,Domain.Entities.Product>();
            CreateMap<ProductViewModel,Domain.Entities.Product>().ReverseMap();
        }
    }
}
