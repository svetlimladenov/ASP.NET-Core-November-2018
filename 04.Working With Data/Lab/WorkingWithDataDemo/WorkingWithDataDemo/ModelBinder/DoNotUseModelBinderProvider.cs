using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WorkingWithDataDemo.ViewModels;

namespace WorkingWithDataDemo.ModelBinder
{
    public class DoNotUseModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(StudentBindingModel))
            {
                //add some logic
                return new GetAllHeadersModelBinder();
            }

            return null;
        }
    }
}
