using System.Linq.Dynamic.Core;
using Trip.Api.Mappers.PropertyMappings;

namespace Trip.Api.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy,
        Dictionary<string, PropertyMappingValue> mappingDict)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (mappingDict == null)
        {
            throw new ArgumentNullException(nameof(mappingDict));
        }

        if (string.IsNullOrWhiteSpace(orderBy))
        {
            return source;
        }

        var orderByStr = string.Empty;
        var orderByAfterSplit = orderBy.Split(',');

        foreach (var order in orderByAfterSplit)
        {
            var trimmedOrder = order.Trim();
            var orderDescending = trimmedOrder.EndsWith(" desc");
            var indexOfFirstSpace = trimmedOrder.IndexOf(" ", StringComparison.Ordinal);
            var propertyName = indexOfFirstSpace == -1 ? trimmedOrder : trimmedOrder.Remove(indexOfFirstSpace);

            if (!mappingDict.ContainsKey(propertyName))
            {
                throw new ArgumentException($"{propertyName}的键映射丢失");
            }

            var propertyMappingValue = mappingDict[propertyName];

            if (propertyMappingValue == null)
            {
                throw new ArgumentNullException(nameof(propertyMappingValue));
            }

            foreach (var destinationProperty in propertyMappingValue.DestinationProperties.Reverse())
            {
                orderByStr = orderByStr + (string.IsNullOrWhiteSpace(orderByStr) ? string.Empty : ", ") +
                    destinationProperty + (orderDescending ? " descending" : " ascending");
            }
        }

        return source.OrderBy(orderByStr);
    }
}