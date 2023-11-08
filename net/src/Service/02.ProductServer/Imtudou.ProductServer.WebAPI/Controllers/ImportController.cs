using Imtudou.Core.Utility.NpoiHelper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Imtudou.ProductServer.WebAPI.Controllers
{
    /// <summary>
    /// 导入
    /// </summary>
    [Route("api/[controller]")]
    public class ImportController : ControllerBase
    {
        private readonly IHostEnvironment _hostEnvironment;

        public ImportController(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        /// <summary>
        /// 个人健康档案导入模板下载 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Import/DowloadExcelTemplate")]
        public async Task<IActionResult> DowloadExcelTemplateAsync()
        {
            var path = Path.Combine(this._hostEnvironment.ContentRootPath, "Configs\\ArchivesExcelTemplate.xlsx");
            string filename = "导入模板_" + Guid.NewGuid().ToString()+ ".xlsx";
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read); // 读入excel模板
            return this.File(file, MediaTypeNames.Application.Octet, filename);
        }

        // <summary>
        /// 导入
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Import/Import")]
        public async Task<bool> ImportAsync([FromForm][Required] string action, [Required] string token, [Required] IFormFile file)
        {
            var stream = file.OpenReadStream();
            var list = new NpoiExcelUtility().UploadFileStream<ImportModel>(stream, 1, 2);
            return true;

        }
    }

    public class ImportModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
