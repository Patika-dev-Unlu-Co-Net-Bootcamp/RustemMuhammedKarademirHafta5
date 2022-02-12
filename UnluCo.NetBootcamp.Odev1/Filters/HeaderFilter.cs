using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnluCo.NetBootcamp.Odev5.Filters
{
    public class HeaderFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Headers.Add("Response Create Date", DateTime.Now.ToString("G"));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}
