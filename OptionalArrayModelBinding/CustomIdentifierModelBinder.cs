﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Threading.Tasks;

namespace OptionalArrayModelBinding
{
    public class CutomIdentifierModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType.IsArray && context.Metadata.ModelType == typeof(CustomIdentifier[]))
            {
                return new ArrayModelBinder<CustomIdentifier>(new CustomIdentifierModelBinder());
            }

            if (context.Metadata.ModelType == typeof(CustomIdentifier))
            {
                return new BinderTypeModelBinder(typeof(CustomIdentifierModelBinder));
            }

            return null;
        }
    }

    public class CustomIdentifierModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

            var parseResult = CustomIdentifier.TryParse(valueProviderResult.FirstValue);
            if (parseResult.Failed)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, parseResult.Message.Message);
            }
            else
            {
                bindingContext.Model  = parseResult.Value;
                bindingContext.Result = ModelBindingResult.Success(parseResult.Value);
            }

            return Task.CompletedTask;
        }
    }
}