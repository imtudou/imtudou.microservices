using heimawebapi.Entity;
using heimawebapi.Model;
using heimawebapi.Service;

using Microsoft.AspNetCore.Mvc;

using NPOI.HPSF;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace heimawebapi.Controllers
{
    /// <summary>
    /// 代金券
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {

        private readonly ISpecillVoucherService _specillVoucherService;
        private readonly IVoucherOrderService _voucherOrderService;

        public VoucherController(
            ISpecillVoucherService specillVoucherService,
            IVoucherOrderService voucherOrderService)
        {
            _specillVoucherService = specillVoucherService;
            _voucherOrderService = voucherOrderService;
        }


        #region 优惠券
        // GET: api/<VoucherController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<VoucherController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        [Route("GetID")]
        public string GetID([FromQuery]int id)
        {
            return "id";
        }

        // POST api/<VoucherController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }



        // PUT api/<VoucherController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VoucherController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        #endregion

        #region 秒杀优惠券

        // POST api/<VoucherController>
        [HttpPost]
        [Route("AddSpecillVoucher")]
        public IActionResult AddSpecillVoucher([FromBody] VoucherOrderAddModel intput)
        {
            // 1. 查询秒杀优惠券(判断秒杀是否开始)
            var specillVoucher = _specillVoucherService.GetSpecillVoucher(intput.VoucherId);
            if (specillVoucher == null)
                return  BadRequest("未找到优惠券！");

            if (DateTime.Now <= specillVoucher.BeginTime)
            {
                return BadRequest("秒杀未开始！");
            }

            if (DateTime.Now >= specillVoucher.EndTime)
            {
                return BadRequest("秒杀已结束！");
            }

            // 判断库存是否充足
            if (specillVoucher.Stock <= 0 )
            {
                return BadRequest("库存不足！");
            }

            string orderid = Guid.NewGuid().ToString();
            var isscuss = _specillVoucherService.EditStock(intput.VoucherId);
            if (isscuss)
            {
                _voucherOrderService.AddVoucherOrder(new Entity.tbVoucherOrderEntity
                {
                    UserID = "zhangsan",
                    VoucherId = intput.VoucherId,
                    PayType = 2.ToString(),
                    Status = 1.ToString(),
                    CreateTime = DateTime.Now,
                    Orderid = orderid
                });
            }
            return Ok(new { msg = "success", Orderid = orderid });
        }
        #endregion
    }
}
