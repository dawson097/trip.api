using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Trip.Api.Helpers;

/// <summary>
/// 数组模型绑定
/// </summary>
public class ArrayModelBinderHelper : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        // 判断模型元数据是否为Enumerable类型
        if (!bindingContext.ModelMetadata.IsEnumerableType)
        {
            // 不为Enumerable类型
            bindingContext.Result = ModelBindingResult.Failed();

            return Task.CompletedTask;
        }

        // 从已绑定的数据上下文中获取模型绑定键名的原始值
        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString();

        // 判断字符串是否为空或空字符串
        if (string.IsNullOrWhiteSpace(value))
        {
            // 为空或空字符串，则返回null
            bindingContext.Result = ModelBindingResult.Success(null);

            return Task.CompletedTask;
        }

        var elementType = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];
        var converter = TypeDescriptor.GetConverter(elementType);

        // 将获取导致的原始值进行转换后将值放入数组中
        var values = value.Split([","], StringSplitOptions.RemoveEmptyEntries)
            .Select(val => converter.ConvertFromString(val.Trim())).ToArray();
        // 根据元素类型及values的长度创建对应类型的数组
        var typedValues = Array.CreateInstance(elementType, values.Length);

        // 将values复制进入数组中
        values.CopyTo(typedValues, 0);
        bindingContext.Model = typedValues;
        bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);

        return Task.CompletedTask;
    }
}