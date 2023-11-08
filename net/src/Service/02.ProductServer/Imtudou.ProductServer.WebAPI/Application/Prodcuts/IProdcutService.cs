using Imtudou.ProductServer.WebAPI.Application.Prodcuts.AddProdcut.Dto;
using Imtudou.ProductServer.WebAPI.Application.Prodcuts.Dto;

using System.Threading.Tasks;

namespace Imtudou.ProductServer.WebAPI.Application.Prodcuts
{
    public interface IProdcutService
    {
        Task<string> AddProductAsync(AddProdcutDto input);
        Task<bool> EditProductNumAsync(EditProductNumDto input);
    }
}
