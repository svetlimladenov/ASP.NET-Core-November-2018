using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WorkingWithDataDemo.ModelBinder
{
    public class GetAllHeadersModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var stringBuilder = new StringBuilder();
            foreach (var header in bindingContext.HttpContext.Request.Headers)
            {
                stringBuilder.AppendLine(header.ToString());
            }

            bindingContext.Result = ModelBindingResult.Success(stringBuilder.ToString());

            return Task.CompletedTask;
        }
    }
}
