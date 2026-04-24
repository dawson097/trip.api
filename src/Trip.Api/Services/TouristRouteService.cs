using System.Dynamic;
using MapsterMapper;
using Microsoft.Extensions.Primitives;
using Trip.Api.Dtos.TouristRoute;
using Trip.Api.Entities;
using Trip.Api.Extensions;
using Trip.Api.Helpers;
using Trip.Api.Repositories.Interfaces;
using Trip.Api.ResourceParameters;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Services;

public class TouristRouteService(
    ICommonRepository<TouristRoute> commonRepository,
    ITouristRouteRepository routeRepository,
    IPropertyMappingService propertyMappingService,
    UrlHelper urlHelper,
    IMapper mapper)
    : CommonService<TouristRoute>(commonRepository), ITouristRouteService
{
    public async Task<(IEnumerable<ExpandoObject>, object)> GetAllRoutesAsync(
        TouristRouteResourceParameters routeParams,
        PaginationResourceParameters paginationParams, StringSegment primaryType)
    {
        var queryRes =
            routeRepository.GetAllRoutesWithQuery(routeParams.Keyword, routeParams.RatingType, routeParams.RatingValue);

        if (!string.IsNullOrWhiteSpace(routeParams.OrderBy))
        {
            var routeMappingDict = propertyMappingService.GetPropertyMapping<TouristRouteDto, TouristRoute>();
            queryRes = queryRes.ApplySort(routeParams.OrderBy, routeMappingDict);
        }

        var routesFromRepo =
            await PaginationHelper<TouristRoute>.CreatePaginationAsync(paginationParams.PageNumber,
                paginationParams.PageSize, queryRes);

        var previousPageLink = routesFromRepo.HasPrevious
            ? urlHelper.GenerateTouristRouteResourceUrl(routeParams, paginationParams, ResourceUriType.PreviousPage)
            : null;
        var nextPageLink = routesFromRepo.HasNext
            ? urlHelper.GenerateTouristRouteResourceUrl(routeParams, paginationParams, ResourceUriType.NextPage)
            : null;
        var paginationMetaData = new
        {
            previousPageLink,
            nextPageLink,
            totalCount = routesFromRepo.TotalCount,
            pageSize = routesFromRepo.PageSize,
            currentPage = routesFromRepo.CurrentPage,
            totalPages = routesFromRepo.TotalPages
        };

        IEnumerable<object> routesDto;
        IEnumerable<ExpandoObject> routesShapedData;

        if (primaryType == "vnd.personal.simplify")
        {
            routesDto = mapper.Map<IEnumerable<TouristRouteSimplifyDto>>(routesFromRepo);
            routesShapedData = ((IEnumerable<TouristRouteSimplifyDto>)routesDto).ShapeDataList(routeParams.Fields);
        }
        else
        {
            routesDto = mapper.Map<IEnumerable<TouristRouteDto>>(routesFromRepo);
            routesShapedData = ((IEnumerable<TouristRouteDto>)routesDto).ShapeDataList(routeParams.Fields);
        }

        return (routesShapedData, paginationMetaData);
    }

    public async Task<ExpandoObject> GetRouteByIdAsync(Guid routeId, string? fields)
    {
        var routeFromRepo = await routeRepository.GetRouteByIdAsync(routeId);
        var routeDto = mapper.Map<TouristRouteDto>(routeFromRepo);

        return routeDto.ShapeData(fields);
    }

    public async Task<TouristRouteUpdateDto> GetPartialUpdateRouteByIdAsync(Guid routeId)
    {
        var routeFromRepo = await routeRepository.GetRouteByIdAsync(routeId);

        return mapper.Map<TouristRouteUpdateDto>(routeFromRepo);
    }


    public async Task<TouristRouteDto> CreateRouteAsync(TouristRouteCreateDto routeCreateDto)
    {
        var routeEntity = mapper.Map<TouristRoute>(routeCreateDto);

        await routeRepository.CreateRouteAsync(routeEntity);
        await routeRepository.SaveAsync();

        return mapper.Map<TouristRouteDto>(routeEntity);
    }

    public async Task UpdateRouteAsync(Guid routeId, TouristRouteUpdateDto routeUpdateDto)
    {
        var routeFromRepo = await routeRepository.GetRouteByIdAsync(routeId);

        mapper.Map(routeUpdateDto, routeFromRepo);
        await routeRepository.SaveAsync();
    }

    public async Task PartialUpdateRouteAsync(Guid routeId)
    {
        var routeFromRepo = await routeRepository.GetRouteByIdAsync(routeId);
        var routeToUpdate = mapper.Map<TouristRouteUpdateDto>(routeFromRepo);

        mapper.Map(routeToUpdate, routeFromRepo);
        await routeRepository.SaveAsync();
    }

    public async Task DeleteRouteAsync(Guid routeId)
    {
        var routeFromRepo = await routeRepository.GetRouteByIdAsync(routeId);

        routeRepository.DeleteRoute(routeFromRepo);
        await routeRepository.SaveAsync();
    }

    public async Task DeleteRoutesAsync(IEnumerable<Guid> routeIds)
    {
        var routeItems = await routeRepository.GetRoutesByIdsAsync(routeIds);

        routeRepository.DeleteRoutes(routeItems);
        await routeRepository.SaveAsync();
    }

    public bool MappingExists(string? fields)
    {
        return propertyMappingService.IsMappingExists<TouristRouteDto, TouristRoute>(fields);
    }

    public bool PropertiesExists(string? fields)
    {
        return propertyMappingService.IsPropertyExists<TouristRouteDto>(fields);
    }
}