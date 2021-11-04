using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AgileConfig.Server.Apisite.Filters
{
    public class ModelVaildateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var arrMsg = context.ModelState.Values.SelectMany(s => s.Errors).Select(s => s.ErrorMessage).Distinct().ToArray();
                var errMsg = string.Join(";", arrMsg);
                context.Result = new JsonResult(new
                {
                    success = false,
                    message = errMsg.ToString()
                });
            }
        }
    }
}
