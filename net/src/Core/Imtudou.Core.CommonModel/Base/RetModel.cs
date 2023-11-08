using Imtudou.Core.CommonEnum;
using Imtudou.Core.CommonEnum.Extensions;

namespace Imtudou.Core.CommonModel.Base
{
    /// <summary>
    /// 统一返回Model
    /// </summary>
    public class RetModel<T>  where T:class,new()
    {
        /// <summary>
        /// 构造
        /// </summary>
        public RetModel()
        {
            this.Code = ResultCodeEnum.Success;
            this.Msg = ResultCodeEnum.Success.GetDescriptionValue();
            this.Data = typeof(T).IsClass ? new T() : default;
        }

        /// <summary>
        /// 构造
        /// </summary>
        public RetModel(ResultCodeEnum code)
        {
            this.Code = ResultCodeEnum.Success;
            this.Msg = ResultCodeEnum.Success.GetDescriptionValue();
            this.Data = default;
        }

        /// <summary>
        /// 返回信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public RetModel<T> SetModel(ResultCodeEnum code, T data = default)
        {
            this.Code = Code;
            this.Msg = code.GetDescriptionValue();
            this.Data = data;
            return this;
        }

        /// <summary>
        /// 返回信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public RetModel<T> SetModel(ResultCodeEnum code, string msg,T data = default)
        {
            this.Code = code;
            this.Msg = msg;
            this.Data = data;
            return this;
        }

        /// <summary>
        /// 接口编码
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 请求来源
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 租户信息
        /// </summary>
        public string TenantID { get; set; }

        /// <summary>
        /// JWT
        /// </summary>
        public string JWT { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string TimeStamp { get; set; }


        /// <summary>
        /// 统一返回码
        /// </summary>
        public ResultCodeEnum Code { get; set; }

        /// <summary>
        /// 统一返回消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 统一返回Data
        /// </summary>
        public T Data { get; set; }

    }
}
