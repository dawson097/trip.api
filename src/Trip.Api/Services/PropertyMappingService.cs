using Trip.Api.Dtos.TouristRoute;
using Trip.Api.Entities;
using Trip.Api.Mappers.PropertyMappings;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Services;

public class PropertyMappingService : IPropertyMappingService
{
    private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

    private readonly Dictionary<string, PropertyMappingValue> _routePropertyMappings =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "Id", new PropertyMappingValue(new List<string> { "Id" }) },
            { "Title", new PropertyMappingValue(new List<string> { "Title" }) },
            { "Rating", new PropertyMappingValue(new List<string> { "Rating" }) },
            { "OriginalPrice", new PropertyMappingValue(new List<string> { "OriginalPrice" }) }
        };

    public PropertyMappingService()
    {
        _propertyMappings.Add(new PropertyMapping<TouristRouteDto, TouristRoute>(_routePropertyMappings));
    }

    public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
    {
        var matchMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

        if (matchMapping.Count() == 1)
        {
            return matchMapping.First()._mappingDict;
        }

        throw new Exception($"无法找到<{typeof(TSource)}>, {typeof(TDestination)}>的映射实例");
    }
}