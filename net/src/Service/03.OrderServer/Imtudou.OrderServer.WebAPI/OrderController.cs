using AutoMapper;

using Imtudou.OrderServer.WebAPI.Application.Order;
using Imtudou.OrderServer.WebAPI.Application.Order.Dto;
using Imtudou.OrderServer.WebAPI.Handler.Command;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text;

namespace Imtudou.OrderServer.WebAPI
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private ILogger<OrderController> logger;
        private IOrderService orderService;
        private readonly IMapper mapper;
        private readonly IMediator _mediator;

        public OrderController(
            ILogger<OrderController> logger,
            IOrderService orderService,
            IMapper mapper,
            IMediator mediator)
        {
            this.logger = logger;
            this.orderService = orderService;
            this.mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderAsync([FromBody] OrderInput input)
        {
            input.Handle();
            var dto = this.mapper.Map<OrderInput, AddOrderDto>(input);
            var result = await orderService.AddOrderAsync(dto);
            return Ok(result);
        }

        public class OrderInput
        {
            public string OrderID { get; set; }
            public string ProductID { get; set; }
            public string UserID { get; set; }
            public int OrderNum { get; set; }

            public void Handle()
            {
                OrderID = Guid.NewGuid().ToString();
                OrderNum = 1;
            }
        }


    }
}
