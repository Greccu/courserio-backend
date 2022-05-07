using Courserio.Core.Constants;
using Courserio.Core.DTOs;
using Courserio.Core.Filters;
using Microsoft.EntityFrameworkCore;

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

        public static async Task<PagedResult<T>> MapToPagedResultAsync<T, TDbSet>(
            this IQueryable<TDbSet> queryableEntities,
            BaseFilter filter,
            Func<TDbSet, T> mapFunc)
        {
            if (queryableEntities is null || !queryableEntities.Any())
            {
                return new PagedResult<T>
                {
                    TotalResults = 0
                };
            }
            if (filter.IsPagingEnabled)
            {
                var page = filter.Page == 0 ? DefaultPage : filter.Page;
                var pageSize = filter.PageSize == 0 ? DefaultPageSize : filter.PageSize;
                var totalCount = await queryableEntities.CountAsync();
                var data = await queryableEntities
                    .Skip(CalculateSkip(filter))
                    .Take(CalculateTake(filter))
                    .ToListAsync();
                return new PagedResult<T>
                {
                    PageSize = pageSize,
                    TotalResults = totalCount,
                    TotalPageNumber = (totalCount - 1) / pageSize + 1,
                    CurrentPageNumber = page,
                    Data = data.ApplyMap(mapFunc)
                };
            }
            var totalCount2 = await queryableEntities.CountAsync();
            var data2 = await queryableEntities
                .ToListAsync();

            return new PagedResult<T>
            {
                TotalResults = totalCount2,
                Data = data2.ApplyMap(mapFunc)
            };
        }

        public static List<T> ApplyMap<T, TDbSet>(this List<TDbSet> list, Func<TDbSet, T> mapFunc)
        {
            return list.Select(mapFunc).ToList();
        }


    }
}
