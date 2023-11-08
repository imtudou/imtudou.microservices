namespace Imtudou.Core.Utility.NpoiHelper
{
    /// <summary>
    /// 表头模型
    /// </summary>
    public class NpoiHeadModel
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FiledName { get; set; }

        /// <summary>
        /// 字段描述
        /// </summary>
        public string FiledDesc { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
