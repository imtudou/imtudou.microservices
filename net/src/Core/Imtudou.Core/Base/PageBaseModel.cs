using Imtudou.Core.CommonEnum;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.Core.Base
{
    /// <summary>
    ///  分页对象
    /// </summary>
    public class PageBaseModel 
    {
        /// <summary>
        /// 页数
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 分页条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public OrderByTypeEnum OrderByType { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderFileds { get; set; }
    }
}
