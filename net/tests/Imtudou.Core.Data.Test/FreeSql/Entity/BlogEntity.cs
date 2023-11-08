using FreeSql.DataAnnotations;

using System;

namespace Imtudou.Core.Data.Test
{
    public class BlogEntity
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int ID { get; set; }
        public Guid BlogID { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
