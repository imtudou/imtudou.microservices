namespace Imtudou.Core.Utility.NpoiHelper
{
    using System.IO;

    /// <summary>
    /// npoi生成xlsx，将MemoryStream写入workbook会自动释放MemoryStream，导致MemoryStream关闭，无法写入HttpHeader
    /// </summary>
    public class XLSXMemoryStream : MemoryStream
    {
        /// <summary>
        /// 能否关闭标识
        /// </summary>
        public bool AllowClose { get; set; }

        /// <summary>
        /// 构造，默认能关闭
        /// </summary>
        public XLSXMemoryStream()
        {
            this.AllowClose = true;
        }

        /// <summary>
        /// 重写关闭方法
        /// </summary>
        public override void Close()
        {
            if (this.AllowClose)
            {
                base.Close();
            }
        }
    }
}
