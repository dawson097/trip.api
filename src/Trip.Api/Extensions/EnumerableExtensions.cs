using System.Dynamic;
using System.Reflection;

namespace Trip.Api.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<ExpandoObject> ShapeDataList<TSource>(this IEnumerable<TSource> sources, string? fields)
    {
        if (sources == null)
        {
            throw new ArgumentNullException(nameof(sources));
        }

        var shapedDataList = new List<ExpandoObject>();
        var propertyInfoList = new List<PropertyInfo>();

        if (string.IsNullOrWhiteSpace(fields))
        {
            // 获取所有的公共属性
            var propertyInfos =
                typeof(TSource).GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            propertyInfoList.AddRange(propertyInfos);
        }
        else
        {
            // 字段通过“,”号分割
            var fieldAfterSplit = fields.Split(',');

            foreach (var field in fieldAfterSplit)
            {
                var propertyName = field.Trim(); // 去除query字段的空字符串
                // 从源对象中获取属性信息 
                var propertyInfo = typeof(TSource).GetProperty(propertyName,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo == null)
                {
                    throw new Exception($"属性({propertyName})找不到({typeof(TSource)})");
                }

                // 将获取到的属性信息放入属性信息集合
                propertyInfoList.Add(propertyInfo);
            }
        }

        foreach (var sourceObj in sources)
        {
            var dataShapedObj = new ExpandoObject();

            foreach (var propertyInfo in propertyInfoList)
            {
                // 从源对象中获取属性值
                var propertyValue = propertyInfo.GetValue(sourceObj);

                // 将query字段加入扩展对象
                ((IDictionary<string, object>)dataShapedObj!).Add(propertyInfo.Name, propertyValue!);
            }

            // 将完成塑性的数据放入塑性数据集合
            shapedDataList.Add(dataShapedObj);
        }

        return shapedDataList;
    }
}