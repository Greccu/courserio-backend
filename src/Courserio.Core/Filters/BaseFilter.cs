using Courserio.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courserio.Core.Filters
{
    public class BaseFilter
    {
        public bool IsPagingEnabled { get; set; } = false;
        public int PageSize { get; set; } = PaginationConstants.DefaultPageSize;
        public int Page { get; set; } = 1;
    }
}
