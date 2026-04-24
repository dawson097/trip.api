namespace Trip.Api.ResourceParameters;

/// <summary>
/// 分页参数处理
/// </summary>
public class PaginationResourceParameters
{
    private int _pageNumber = 1;
    private int _pageSize = 10;

    public int PageSize
    {
        get => _pageSize;
        set
        {
            const int maxPageSize = 100;

            if (value >= 1)
            {
                _pageSize = value > maxPageSize ? maxPageSize : value;
            }
        }
    }

    public int PageNumber
    {
        get => _pageNumber;
        set
        {
            if (value >= 1)
            {
                _pageNumber = value;
            }
        }
    }
}