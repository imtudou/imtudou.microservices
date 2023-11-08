using AutoMapper;

using Imtudou.ProductServer.WebAPI.Application.Prodcuts.AddProdcut.Dto;
using Imtudou.ProductServer.WebAPI.Application.Prodcuts.Dto;
using Imtudou.ProductServer.WebAPI.Controllers;

using static Imtudou.ProductServer.WebAPI.Controllers.ProductController;

namespace Imtudou.ProductServer.WebAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddProdcutInput, AddProdcutDto>();
            CreateMap<EditProductNumInput, EditProductNumDto>();
            
        }
    }
}
