using Courserio.Core.Constants;
using Courserio.Core.Filters;
using Courserio.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courserio.Pagination
{
    public static class PaginationHelper
    {
        public const int DefaultPage = 1;
        public const int DefaultPageSize = PaginationConstants.DefaultPageSize;

        public static int CalculateTake(int pageSize)
        {
            return pageSize <= 0 ? DefaultPageSize : pageSize;
        }
        public static int CalculateSkip(int pageSize, int page)
        {
            page = page <= 0 ? DefaultPage : page;

            return CalculateTake(pageSize) * (page - 1);
        }

        public static int CalculateTake(BaseFilter baseFilter)
        {
            return CalculateTake(baseFilter.PageSize);
        }
        public static int CalculateSkip(BaseFilter baseFilter)
        {
            return CalculateSkip(baseFilter.PageSize, baseFilter.Page);
        }

        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> entities, BaseFilter filter)
        {
            return entities.Skip(CalculateSkip(filter.PageSize, filter.Page)).Take(CalculateTake(filter.PageSize));
        }
    }
}
