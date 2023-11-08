using Imtudou.Core.Cache;
using Imtudou.Core.Cache.StackExchangeRedis;
using Imtudou.OrderServer.WebAPI.Application.Order.Dto;
using Imtudou.OrderServer.WebAPI.Handler.Command;

using MediatR;

using Newtonsoft.Json;

namespace Imtudou.OrderServer.WebAPI.Application.Order
{
    public class OrderService : IOrderService
    {
        private readonly IRedisCache redisHelper;
        private readonly IMediator _mediator;
        public OrderService(IMediator mediator)
        {
            this.redisHelper = new RedisCache(new RedisOptions("127.0.0.1:6379,allowadmin=true,connectTimeout=1000,connectRetry=1,syncTimeout=10000", 0)); ; ;
            _mediator = mediator;
        }

        public async Task<string> AddOrderAsync(AddOrderDto input)
        {
            if (await this.redisHelper.KeyExistsAsync("product:AddOrderAsync"))
            {
                var orderList = await this.redisHelper.PeekRangeAsync<AddOrderDto>("product:AddOrderAsync", 0);
                if (orderList.Any(s => s.UserID == input.UserID))
                {
                    return $"用户：{input.UserID}已下单!";
                } 
            }

            await this.redisHelper.EnqueueAsync<AddOrderDto>("product:AddOrderAsync", input);
            await this._mediator.Publish(new EditProductNumCommand { ProductID = input.ProductID });
            return input.OrderID;

        }
    }
}
