

using Imtudou.Core.Data;

using SqlSugar;

namespace Imtudou.ProductServer.WebAPI.Domin.Entity
{

    //实体与数据库结构一样
    [SugarTable("business_order")]
    public class ProductEntity : IKey<int>
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
         public string ProductID { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public int Count { get; set; }

      
}
}
