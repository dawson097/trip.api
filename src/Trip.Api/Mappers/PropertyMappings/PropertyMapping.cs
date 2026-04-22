using Trip.Api.Entities;

namespace Trip.Api.Mappers.PropertyMappings;

public class PropertyMapping<TSource, TDestination> : IPropertyMapping
{
    public PropertyMapping(Dictionary<string, PropertyMappingValue> mappingDict)
    {
        _mappingDict = mappingDict;
    }

    public Dictionary<string, PropertyMappingValue> _mappingDict { get; set; }
}