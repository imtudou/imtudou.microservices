using Imtudou.Core.Cache;
using Imtudou.Core.Cache.StackExchangeRedis;
using Imtudou.ProductServer.WebAPI.Application.Prodcuts.AddProdcut.Dto;
using Imtudou.ProductServer.WebAPI.Application.Prodcuts.Dto;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imtudou.ProductServer.WebAPI.Application.Prodcuts
{
    public class ProductService : IProdcutService
    {
        private readonly IRedisCache redisHelper;

        public ProductService()
        {
            this.redisHelper = this.redisHelper = new RedisCache(new RedisOptions("127.0.0.1:6379,allowadmin=true,connectTimeout=1000,connectRetry=1,syncTimeout=10000", 0)); ;
        }

        public async Task<string> AddProductAsync(AddProdcutDto input)
        {
            var concurrentDictionary = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();
            var t = typeof(AddProdcutDto);
            foreach (var item in t.GetProperties())
            {
                concurrentDictionary.TryAdd($"{item.Name}", item.GetValue(input).ToString());                               
            };
            await redisHelper.HashSetAsync("product:AddProductAsync", concurrentDictionary);
            return input.ProductID;

        }

        public async Task<bool> EditProductNumAsync(EditProductNumDto input)
        {
            var product = await redisHelper.HashGetAsync("product:AddProductAsync");
            if (product.Count > 0)
            {
                var count = product.GetValueOrDefault($"{nameof(AddProdcutDto.Count)}");
                int productTotalCount = Convert.ToInt32(count);
                if (productTotalCount > 0)
                {
                    int newValue = productTotalCount - 1;
                    int compairValue = productTotalCount;
                    var issuccess = product.TryUpdate($"{nameof(AddProdcutDto.Count)}", newValue.ToString(), compairValue.ToString());
                    await redisHelper.HashSetAsync("product:AddProductAsync", product);
                    return issuccess;
                }                
            }
            return false;
        }
    }
}
