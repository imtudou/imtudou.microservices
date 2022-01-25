using System;
using System.ComponentModel;

namespace Imtudou.Core.CommonEnum
{
    /*
     * 
     *  200 - 请求成功
        301 - 资源（网页等）被永久转移到其它URL
        404 - 请求的资源（网页等）不存在
        500 - 内部服务器错误



     * 分类    区间        描述
     * 1xxx   1000~1999   信息,服务器收到请求,需要请求者继续执行操作
     * 2xxx   2000~2999   成功,操作被成功接收并处理
     * 3xxx   3000~3999   重定向,需要进一步的操作以完成请求
     * 4xxx   4000~4999   客户端错误,请求包含语法错误或无法完成请求
     * 5xxx   5000~5999   服务器错误,服务器在处理请求的过程中发生了错误
     
     
     */
    /// <summary>
    /// 统一返回码
    /// </summary>
    public enum ResultCodeEnum
    {
        /// <summary>
        /// 请求成功
        /// </summary>
        [Description("请求成功")]
        Success = 100200,

        /// <summary>
        /// 请求资源不存在
        /// </summary>
        [Description("请求资源不存在")]
        UnExist = 100404,

        /// <summary>
        /// 服务器错误
        /// </summary>
        [Description("服务器错误")]
        InsideError = 100500,

        /// <summary>
        /// 版本错误
        /// </summary>
        [Description("版本错误")]
        VersionError = 500001,

        /// <summary>
        /// 无返回数据
        /// </summary>
        [Description("无返回数据")]
        NoInfo = 500002,

        /// <summary>
        /// 操作失败
        /// </summary>
        [Description("操作失败")]
        OperatorFail = 500003,

        /// <summary>
        /// IP限制
        /// </summary>
        [Description("IP限制")]
        Iplimit = 500004,

        /// <summary>
        /// 非法操作或没有权限
        /// </summary>
        [Description("非法操作或没有权限")]
        IllegalOperation = 500005,

        /// <summary>
        /// 对象只读
        /// </summary>
        [Description("对象只读")]
        ObjReadOnly = 500006,

        /// <summary>
        /// 超时
        /// </summary>
        [Description("超时")]
        TimeOut = 500007,

        /// <summary>
        /// 非法字符
        /// </summary>
        [Description("非法字符")]
        UnlawfulChar = 500008,

        /// <summary>
        /// 验证失败
        /// </summary>
        [Description("验证失败")]
        VerificationFail = 500009,

        /// <summary>
        /// 参数错误
        /// </summary>
        [Description("参数错误")]
        ParamError = 500010,

        /// <summary>
        /// 网络问题
        /// </summary>
        [Description("网络问题")]
        NetworkTrouble = 500011,

        /// <summary>
        /// 频率控制
        /// </summary>
        [Description("频率控制")]
        FrequencyLimit = 500012,

        /// <summary>
        /// 重复操作
        /// </summary>
        [Description("重复操作")]
        RepeatOperation = 500013,

        /// <summary>
        /// 通道不可用
        /// </summary>
        [Description("通道不可用")]
        PassagewayFail = 500014,

        /// <summary>
        /// 数据已使用
        /// </summary>
        [Description("数据已使用")]
        DataUsing = 500015,

        /// <summary>
        /// 数量超过上限
        /// </summary>
        [Description("数量超过上限")]
        QuantityLimit = 500016,

        /// <summary>
        /// 账号被禁用
        /// </summary>
        [Description("账号被禁用")]
        AccountProhibition = 500017,

        /// <summary>
        /// 用户名或密码错误
        /// </summary>
        [Description("用户名或密码错误")]
        AccountFail = 500018,

        /// <summary>
        /// 账号在其他地方登陆
        /// </summary>
        [Description("账号在其他地方登陆")]
        LoginOtherArea = 500019,

        /// <summary>
        /// 密码已过期
        /// </summary>
        [Description("密码已过期")]
        PasswordExpired = 500020,

        /// <summary>
        /// 微信未绑定
        /// </summary>
        [Description("微信未绑定")]
        WeChatUnbind = 500021,

        /// <summary>
        /// 数据冲突
        /// </summary>
        [Description("数据冲突")]
        DataConflict = 500022
    }
}
