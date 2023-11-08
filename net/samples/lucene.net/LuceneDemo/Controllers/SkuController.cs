using Imtudou.Core.CommonEnum;
using Imtudou.Core.Data;
using Imtudou.Core.Data.SqlSugar;
using Imtudou.Core.Helpers;

using LuceneDemo.Entity;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LuceneDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkuController : ControllerBase
    {
        private readonly ISqlSugarRepository<SkuEntity, string> _skuRepository;

        public SkuController(ISqlSugarRepository<SkuEntity, string> skuRepository)
        {
            this._skuRepository = skuRepository.Instance(new SqlOptions(AppSettingsHelper.Configuration.GetConnectionString("mysql"), DataBaseTypeEnum.MySql)) as ISqlSugarRepository<SkuEntity, string>;
        }

        [HttpGet]
        [Route("GetSkuList")]
        public IActionResult GetSkuList([FromQuery]string name)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var skuList = new List<SkuEntity>();
            if (string.IsNullOrEmpty(name))
                skuList = this._skuRepository.GetList(s => true);
            else
                skuList = this._skuRepository.GetList(s => s.Name.Contains(name));

            sw.Stop();
            return Ok(new {msg = $"耗时：{sw.ElapsedMilliseconds} ms",total = skuList.Count});
        }
    }
}
