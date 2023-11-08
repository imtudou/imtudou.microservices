
using Imtudou.Core.Base;
using Imtudou.Core.Logs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.Core.Extensions.Filters
{
    public class APIActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Method.ToUpper() == "POST")
            {
                var ret = new RetModel<object>();
                if (context.ActionArguments.FirstOrDefault().Value != null)
                {
                    if (context.HttpContext.Request.Method == HttpMethod.Post.Method)
                    {
                        if (!context.ModelState.IsValid)
                        {
                            var errMsg = context.ModelState.Values.SelectMany(p => p.Errors).Select(p => p.ErrorMessage).Distinct().ToList();
                            ret.SetModel(CommonEnum.ResultCodeEnum.ParamError, errMsg);
                            context.Result = new JsonResult(ret);
                        }
                    }
                }
                else
                {
                    ret.SetModel(CommonEnum.ResultCodeEnum.ParamError);
                    context.Result = new JsonResult(ret);
                }

                if (context.ActionArguments != null)
                {
                    object param = context.ActionArguments.Values?.FirstOrDefault();

#if release
                NLogHelper.Info(param, $"入参：{context.HttpContext.Request.Host}{context.HttpContext.Request.Path}【{context.HttpContext.Request.GetHashCode()}】\n\t");
#endif
                }
            }
            
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

            if (context.Result != null && context.ModelState.IsValid && context.Exception == null)
            {
                object param = (context.Result as ObjectResult)?.Value;
#if release
                NLogHelper.Info(param, $"出参：{context.HttpContext.Request.Host}{context.HttpContext.Request.Path}【{context.HttpContext.Request.GetHashCode()}】\n\t");
#endif
            }
        }
    }
}
