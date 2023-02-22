using AutoMapper;
using Wholesaler.DataTransferObject;
using Wholesaler.Models;

namespace Wholesaler.Mapper
{
    public class WholesalerMappingProfile : Profile
    {
        public WholesalerMappingProfile()
        {
            CreateMap<CreateProductDto, Product>();

            CreateMap<CreateStorageDto, Storage>();

            CreateMap<CreateUserDto, User>();
        }
    }
}
