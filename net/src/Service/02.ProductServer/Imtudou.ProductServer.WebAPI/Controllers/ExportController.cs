using Imtudou.Core.Base;
using Imtudou.Core.Utility.NpoiHelper;

using Microsoft.AspNetCore.Mvc;

using NPOI.HPSF;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

using System.Threading.Tasks;

namespace Imtudou.ProductServer.WebAPI.Controllers
{
    /// <summary>
    /// 导出
    /// </summary>
    [Route("api/[controller]")]
    public class ExportController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> ExportAsync([FromBody] ExportInput input)
        {
            var data = new List<string> { "aa", "bb", "cc", "dd" };
            var exportData = data.Select(s => new ExportOutput
            {
                ID =  1,
                Name = s,
            }).ToList();

            var heards = new List<NpoiHeadModel>()
            {
                new NpoiHeadModel { FiledDesc = "档案号", FiledName = nameof(ExportOutput.ID), Sort = 1 },
                new NpoiHeadModel { FiledDesc = "姓名", FiledName = nameof(ExportOutput.Name), Sort = 2 }
            };

            var stream = await new NpoiExcelUtility().DownloadFileStreamAsync<ExportOutput>(heards, exportData);
            return this.File(stream, MediaTypeNames.Application.Octet, $"{input.appId}.csv");
        }

    }

    public class ExportInput
    {
        public string env { get; set; }
        public string appId { get; set; }
    }

    public class ExportOutput
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
