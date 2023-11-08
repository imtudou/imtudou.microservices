using AutoMapper;

using Imtudou.OrderServer.WebAPI.Application.Order.Dto;

using static Imtudou.OrderServer.WebAPI.OrderController;

namespace Imtudou.OrderServer.WebAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<OrderInput, AddOrderDto>();
            CreateMap<OrderInput, AddOrderDto>();
        }
    }
}
