using AutoMapper;

using Imtudou.Core.Cache;
using Imtudou.ProductServer.WebAPI.Application.Prodcuts;
using Imtudou.ProductServer.WebAPI.Application.Prodcuts.AddProdcut.Dto;
using Imtudou.ProductServer.WebAPI.Application.Prodcuts.Dto;

using Microsoft.AspNetCore.Mvc;

using NPOI.Util;

using System;
using System.Net.Http.Json;
using System.Threading.Tasks; 

namespace Imtudou.ProductServer.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController: ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IProdcutService prodcutService;

        public ProductController(IMapper mapper, IProdcutService prodcutService)
        {
            this.mapper = mapper;
            this.prodcutService = prodcutService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProdcutAsync([FromBody] AddProdcutInput input)
        {
            input.Handle();
            var dto = this.mapper.Map<AddProdcutInput, AddProdcutDto>(input);
            var result = await prodcutService.AddProductAsync(dto);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditProductNumAsync([FromBody] EditProductNumInput input)
        {
            var dto = this.mapper.Map<EditProductNumInput, EditProductNumDto>(input);
            var result = await prodcutService.EditProductNumAsync(dto);
            return Ok(result);
        }
    }

    public class AddProdcutInput
    {
        public string ProductID { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public int Count { get; set; }

        public void Handle()
        {
            this.ProductID = Guid.NewGuid().ToString();
            this.ProductNo = "P" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }

    public class EditProductNumInput
    {
        public string ProductID { get; set; }
    }
}
