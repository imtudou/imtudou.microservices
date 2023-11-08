using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.Core.Base
{
    /// <summary>
    /// 基础实体
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 创建人ID
        /// </summary>
        public string CreateId { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string CreateName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public string UpdateId { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string UpdateName { get; set; }

        /// <summary>                                        `
        /// 创建时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
    }
}
