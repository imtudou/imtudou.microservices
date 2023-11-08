using FreeSql.DataAnnotations;

using System;

namespace Imtudou.Core.Data.Test
{
    public class BlogDetailEntity
    {
        public BlogDetailEntity(Guid blogid)
        {
            BlogDetailID = Guid.NewGuid();
            Title = "标题" + new Random().Next(1, 99);
            BlogID = blogid;
            Content = "内容" + new Random().Next(1, 99);
            CreateTime = DateTime.Now;
        }

        [Column(IsIdentity = true, IsPrimary = true)]
        public int ID { get; set; }
        public Guid BlogID { get; set; }
        public Guid BlogDetailID { get; set; }              
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
