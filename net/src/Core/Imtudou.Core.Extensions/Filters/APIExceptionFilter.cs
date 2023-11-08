using Imtudou.Core.Base;
using Imtudou.Core.CommonEnum.Extensions;
using Imtudou.Core.Logs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.Core.Extensions.Filters
{
    public class APIExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var ret = new RetModel<object>()
            { 
                Code = CommonEnum.ResultCodeEnum.InsideError,
                Msg = CommonEnum.ResultCodeEnum.InsideError.GetDescriptionValue()
            };
            NLogHelper.Error(context.Exception, $"错误：{context.HttpContext.Request.Host}{context.HttpContext.Request.Path}【{context.HttpContext.Request.GetHashCode()}】");
            context.Result = new JsonResult(ret);
        }
    }
}
