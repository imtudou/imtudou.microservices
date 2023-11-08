using Imtudou.Core.Base;
using Imtudou.Core.Data;

using SqlSugar;

namespace LuceneDemo.Entity
{
    //实体与数据库结构一样
    [SugarTable("tb_sku")]
    public class SkuEntity : IKey<string>
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
        public string Id { get; set; }

        [SugarColumn(ColumnName = "name")]
        public string Name { get; set; }

        [SugarColumn(ColumnName = "price")]
        public string Price { get; set; }

        [SugarColumn(ColumnName = "num")]
        public string Num { get; set; }

        [SugarColumn(ColumnName = "image")]
        public string Image { get; set; }

        [SugarColumn(ColumnName = "category_name")]
        public string CategoryName { get; set; }

        [SugarColumn(ColumnName = "brand_name")]
        public string BrandName { get; set; }

        [SugarColumn(ColumnName = "spec")]
        public string Spec { get; set; }

        [SugarColumn(ColumnName = "sale_num")]
        public string SaleNum { get; set; }

      
    }
   
}
