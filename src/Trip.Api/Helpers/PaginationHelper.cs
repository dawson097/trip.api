using Microsoft.EntityFrameworkCore;

namespace Trip.Api.Helpers;

/// <summary>
/// 分页
/// </summary>
public class PaginationHelper<T> : List<T>
{
    public PaginationHelper(int currentPage, int pageSize, List<T> pageItems)
    {
        CurrentPage = currentPage;
        PageSize = pageSize;
        AddRange(pageItems);
    }

    public int CurrentPage { get; set; }

    public int PageSize { get; set; }

    /// <summary>
    /// 创建分页
    /// </summary>
    /// <param name="currentPage">当前页数</param>
    /// <param name="pageSize">分页显示数据量</param>
    /// <param name="queryRes">分页查询数据</param>
    /// <returns></returns>
    public static async Task<PaginationHelper<T>> CreateAsync(int currentPage, int pageSize, IQueryable<T> queryRes)
    {
        // 获取跳过的页数
        var skipPage = (currentPage - 1) * pageSize;
        queryRes = queryRes.Skip(skipPage); // 跳至指定页数
        queryRes = queryRes.Take(pageSize); // 获取该页数的数据

        var pageItems = await queryRes.ToListAsync();

        return new PaginationHelper<T>(currentPage, pageSize, pageItems);
    }
}