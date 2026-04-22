using System.Dynamic;
using System.Reflection;

namespace Trip.Api.Extensions;

public static class ObjectExtensions
{
    public static ExpandoObject ShapeData<TSource>(this TSource source, string fields)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        var shapedData = new ExpandoObject();

        if (string.IsNullOrWhiteSpace(fields))
        {
            // 获取所有的公共属性
            var propertyInfos = typeof(TSource)
                .GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            foreach (var propertyInfo in propertyInfos)
            {
                // 从源对象中获取属性值
                var propertyValue = propertyInfo.GetValue(source);

                // 将query字段加入扩展对象
                ((IDictionary<string, object>)shapedData!).Add(propertyInfo.Name, propertyValue!);
            }

            return shapedData;
        }

        // 字段通过“,”号分割
        var fieldsAfterSplit = fields.Split(',');

        foreach (var field in fieldsAfterSplit)
        {
            var propertyName = field.Trim(); // 去除query字段的空字符串
            // 从源对象中获取属性信息
            var propertyInfo = typeof(TSource).GetProperty(propertyName,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null)
            {
                throw new Exception($"Property {propertyName} wasn't found on {typeof(TSource)}");
            }

            // 从源对象中获取属性值
            var propertyValue = propertyInfo.GetValue(source);

            // 将query字段加入扩展对象
            ((IDictionary<string, object>)shapedData!).Add(propertyInfo.Name, propertyValue!);
        }

        return shapedData;
    }
}