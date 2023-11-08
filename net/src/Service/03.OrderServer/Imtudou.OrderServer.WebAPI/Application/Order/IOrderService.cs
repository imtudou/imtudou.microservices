using Imtudou.OrderServer.WebAPI.Application.Order.Dto;

namespace Imtudou.OrderServer.WebAPI.Application.Order
{
    public interface IOrderService
    {
        Task<string> AddOrderAsync(AddOrderDto input);
    }
}
