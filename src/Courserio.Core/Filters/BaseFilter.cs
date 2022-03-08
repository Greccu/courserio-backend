using Courserio.Core.Constants;

namespace Courserio.Core.Filters
{
    public class BaseFilter
    {
        public bool IsPagingEnabled { get; set; } = false;
        public int PageSize { get; set; } = PaginationConstants.DefaultPageSize;
        public int Page { get; set; } = 1;
    }
}
