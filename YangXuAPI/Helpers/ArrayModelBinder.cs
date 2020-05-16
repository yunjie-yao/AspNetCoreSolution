using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace YangXuAPI.Helpers
{
    public class ArrayModelBinder:IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bingingContext)
        {
            if (!bingingContext.ModelMetadata.IsEnumerableType)
            {
                bingingContext.Result=ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            var value = bingingContext.ValueProvider.GetValue(bingingContext.ModelName).ToString();
            if (string.IsNullOrWhiteSpace(value))
            {
                bingingContext.Result=ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            var elementType = bingingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];
            var converter = TypeDescriptor.GetConverter(elementType);
            var values = value.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => converter.ConvertFromString(x.Trim())).ToArray();
            var typedValue = Array.CreateInstance(elementType, values.Length);
            values.CopyTo(typedValue,0);

            bingingContext.Model = typedValue;
            bingingContext.Result=ModelBindingResult.Success(bingingContext.Model);

            return Task.CompletedTask;
        }
    }
}
