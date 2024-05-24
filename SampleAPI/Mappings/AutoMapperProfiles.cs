using AutoMapper;
using SampleAPI.Entities;
using SampleAPI.Requests;

namespace SampleAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CreateOrderRequest, Order>().ReverseMap();
            CreateMap<OrderRequestList, Order>().ReverseMap();
        }
    }
}
