namespace Trip.Api.Mappers.PropertyMappings;

public class PropertyMappingValue(IEnumerable<string> destinationProperties)
{
    public IEnumerable<string> DestinationProperties { get; private set; } = destinationProperties;
}