using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courserio.Core.DTOs
{
    public class PagedResult<T>
    {
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
        public int TotalPageNumber { get; set; }
        public int CurrentPageNumber { get; set; }
        public IEnumerable<T> Data { get; set; }
    }

}
