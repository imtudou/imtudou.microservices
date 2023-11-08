using Imtudou.Core.CommonEnum;
using Imtudou.Core.Data;
using Imtudou.Core.Data.SqlSugar;
using Imtudou.Core.Helpers;

using LuceneDemo.Entity;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Lucene.Net;
using Lucene.Net.Documents;
using static Lucene.Net.Documents.Field;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Store;
using Lucene.Net.Index;

namespace LuceneDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndexManageController : ControllerBase
    {
        private readonly ISqlSugarRepository<SkuEntity, string> _skuRepository;

        public IndexManageController(ISqlSugarRepository<SkuEntity, string> skuRepository)
        {
            _skuRepository = skuRepository.Instance(new SqlOptions(AppSettingsHelper.Configuration.GetConnectionString("mysql"), DataBaseTypeEnum.MySql)) as ISqlSugarRepository<SkuEntity, string>;
        }


        /// <summary>
        /// 创建索引
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateIndex")]
        public IActionResult CreateIndex()
        {
            // 1. 获取数据
            var skuList = this._skuRepository.GetList(s => true);

            // 2.创建文档对象
            var docList = new List<Document>();
            foreach (var sku in skuList)
            {
                var doc = new Document();
                doc.Add(new Field(nameof(SkuEntity.Id), $"{sku.Id}", Store.YES, Lucene.Net.Documents.Field.Index.NO));
                doc.Add(new Field(nameof(SkuEntity.Name), $"{sku.Name}", Store.YES, Lucene.Net.Documents.Field.Index.ANALYZED));
                doc.Add(new Field(nameof(SkuEntity.Price), $"{sku.Price}", Store.YES, Lucene.Net.Documents.Field.Index.NO));
                doc.Add(new Field(nameof(SkuEntity.Num), $"{sku.Num}", Store.YES, Lucene.Net.Documents.Field.Index.NO));
                doc.Add(new Field(nameof(SkuEntity.Image), $"{sku.Image}", Store.YES, Lucene.Net.Documents.Field.Index.NO));
                doc.Add(new Field(nameof(SkuEntity.CategoryName), $"{sku.CategoryName}", Store.YES, Lucene.Net.Documents.Field.Index.NO));
                doc.Add(new Field(nameof(SkuEntity.BrandName), $"{sku.BrandName}", Store.YES, Lucene.Net.Documents.Field.Index.NO));
                doc.Add(new Field(nameof(SkuEntity.Spec), $"{sku.Spec}", Store.YES, Lucene.Net.Documents.Field.Index.NO));
                doc.Add(new Field(nameof(SkuEntity.SaleNum), $"{sku.SaleNum}", Store.YES, Lucene.Net.Documents.Field.Index.NO));
                docList.Add(doc);
            }

            // 3. 创建索引库存储位置
            var indexPath = System.IO.Directory.GetCurrentDirectory() + "/Dic";
            if (!System.IO.Directory.Exists(indexPath)) 
                System.IO.Directory.CreateDirectory(indexPath);

            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NativeFSLockFactory());
            if (IndexWriter.IsLocked(directory))
            {
                //  如果索引目录被锁定（比如索引过程中程序异常退出），则首先解锁
                //  Lucene.Net在写索引库之前会自动加锁，在close的时候会自动解锁
                IndexWriter.Unlock(directory);
            }

            //4. 创建分词器 也可用盘古分词
            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_CURRENT);
            IndexWriter indexWriter = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED);
            //IndexWriter indexWriter = new IndexWriter(directory, new PanGuAnalyzer(), true, IndexWriter.MaxFieldLength.UNLIMITED);

            //5. 写入索引库
            foreach (var item in docList)
            {
                indexWriter.AddDocument(item);
            }

            indexWriter.Close(); // Close后自动对索引库文件解锁 
            directory.Close();  // 不要忘了Close，否则索引结果搜不到
            return Ok("索引创建完成！");
        }


        [HttpPost]
        public IActionResult EditIndex()
        {

            return Ok();
        }


        [HttpDelete]
        public IActionResult RemoveIndex()
        {
            return Ok();
        }
    }
}
