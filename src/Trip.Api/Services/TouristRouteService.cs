using MapsterMapper;
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
    LinkGenerator linkGenerator,
    IHttpContextAccessor httpContextAccessor,
    IPropertyMappingService propertyMappingService,
    IMapper mapper)
    : CommonService<TouristRoute>(commonRepository), ITouristRouteService
{
    public async Task<(List<TouristRouteDto>, object)> GetAllRoutesAsync(
        TouristRouteResourceParameters routeParams,
        PaginationResourceParameters paginationParams)
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
            ? GenerateTouristRouteResourceUrl(routeParams, paginationParams, ResourceUriHelper.PreviousPage)
            : null;
        var nextPageLink = routesFromRepo.HasNext
            ? GenerateTouristRouteResourceUrl(routeParams, paginationParams, ResourceUriHelper.NextPage)
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


        var routeDtos = mapper.Map<List<TouristRouteDto>>(routesFromRepo);

        return (routeDtos, paginationMetaData);
    }

    public async Task<TouristRouteDto> GetRouteByIdAsync(Guid routeId)
    {
        var routeFromRepo = await routeRepository.GetRouteByIdAsync(routeId);

        return mapper.Map<TouristRouteDto>(routeFromRepo);
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


    private string GenerateTouristRouteResourceUrl(TouristRouteResourceParameters routeParameters,
        PaginationResourceParameters paginationParams, ResourceUriHelper uriHelper)
    {
        return (uriHelper switch
        {
            ResourceUriHelper.PreviousPage => linkGenerator.GetUriByRouteValues(httpContextAccessor.HttpContext!,
                "GetTouristRoutesAsync",
                new
                {
                    keyword = routeParameters.Keyword,
                    ratingType = routeParameters.RatingType,
                    pageSize = paginationParams.PageSize,
                    pageNumber = paginationParams.PageNumber,
                    orderBy = routeParameters.OrderBy
                }),
            ResourceUriHelper.NextPage => linkGenerator.GetUriByRouteValues(httpContextAccessor.HttpContext!,
                "GetTouristRoutesAsync",
                new
                {
                    keyword = routeParameters.Keyword,
                    ratingType = routeParameters.RatingType,
                    pageSize = paginationParams.PageSize,
                    pageNumber = paginationParams.PageNumber + 1,
                    orderBy = routeParameters.OrderBy
                }),
            _ => linkGenerator.GetUriByRouteValues(httpContextAccessor.HttpContext!, "GetTouristRoutesAsync",
                new
                {
                    keyword = routeParameters.Keyword,
                    ratingType = routeParameters.RatingType,
                    pageSize = paginationParams.PageSize,
                    pageNumber = paginationParams.PageNumber,
                    orderBy = routeParameters.OrderBy
                })
        })!;
    }
}