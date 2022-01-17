using System.ComponentModel;

namespace Imtudou.Core.CommonEnum
{
    /// <summary>
    ///  GB/T 2261.1-2003人的性别代码
    /// </summary>
    public enum GenderEnum
    {
        /// <summary>
        ///  未知的性别
        /// </summary>
        [Description("未知的性别")]
        未知的性别 = 0,

        /// <summary>
        ///  男性
        /// </summary>
        [Description("男性")]
        男性 = 1,

        /// <summary>
        ///  女性
        /// </summary>
        [Description("女性")]
        女性 = 2,

        /// <summary>
        ///  未说明的性别
        /// </summary>
        [Description("未说明的性别")]
        未说明的性别 = 9,
    }
}
