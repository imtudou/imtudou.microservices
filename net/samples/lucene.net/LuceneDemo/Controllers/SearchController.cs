using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;

using LuceneDemo.Entity;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

namespace LuceneDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {

        /// <summary>
        /// 创建索引
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("SearchIndex")]
        public IActionResult SearchIndex([FromQuery] string name, [FromQuery] int pageindex, [FromQuery] int pagesize)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var skuList = new List<SkuEntity>();

            //1. 创建分词对象
            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_CURRENT);

            //创建搜索解析器 
            QueryParser queryParser = new QueryParser(Lucene.Net.Util.Version.LUCENE_CURRENT,nameof(SkuEntity.Name), analyzer);
            var query = queryParser.Parse(name);

            var indexPath = System.IO.Directory.GetCurrentDirectory() + "/Dic";

            // 创建Directory流对象,声明索引库位置
            var directory = Lucene.Net.Store.FSDirectory.Open(indexPath);
            var indexReader = IndexReader.Open(directory, true);

            //创建IndexSearcher准备进行搜索。
            IndexSearcher searcher = new IndexSearcher(indexReader);

            // 使用索引搜索对象，执行搜索，返回结果集TopDocs
            // 第一个参数：搜索对象，第二个参数：返回的数据条数，指定查询结果最顶部的n条数据返回
            TopDocs topDocs = searcher.Search(query, pagesize);

            // 获取查询结果集 解析结果集
            foreach (var item in topDocs.ScoreDocs)
            {
                int docid = item.Doc;
                var doc = searcher.Doc(docid);

                var sku = new SkuEntity();
                sku.Id = doc.Get(nameof(SkuEntity.Id));
                sku.Name = doc.Get(nameof(SkuEntity.Name));
                sku.Price = doc.Get(nameof(SkuEntity.Price));
                sku.Num = doc.Get(nameof(SkuEntity.Num));
                sku.Image = doc.Get(nameof(SkuEntity.Image));
                sku.CategoryName = doc.Get(nameof(SkuEntity.CategoryName));
                sku.BrandName = doc.Get(nameof(SkuEntity.BrandName));
                sku.Spec = doc.Get(nameof(SkuEntity.Spec));
                sku.SaleNum = doc.Get(nameof(SkuEntity.SaleNum));
                skuList.Add(sku);
            }


            indexReader.Clone();

            sw.Stop();
            return Ok(new { msg = $"耗时：{sw.ElapsedMilliseconds} ms", total = skuList.Count});
        }
    }
}
