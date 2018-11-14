using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MiddlewareAndFiltersDemo.Filters
{
    public class MyResultFilter : Attribute, IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("X-PLSWORK","WORKING");
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            
        }
    }
}
