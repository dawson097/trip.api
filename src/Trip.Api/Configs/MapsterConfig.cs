using System.Reflection;
using Mapster;

namespace Trip.Api.Configs;

/// <summary>
/// Mapster配置
/// </summary>
public static class MapsterConfig
{
    public static void Configure()
    {
        // 配置文件自动扫描
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

        TypeAdapterConfig.GlobalSettings.Default
            .NameMatchingStrategy(NameMatchingStrategy.Flexible)
            .IgnoreNullValues(true);
    }
}