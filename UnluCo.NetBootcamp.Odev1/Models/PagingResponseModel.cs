using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnluCo.NetBootcamp.Odev5.Models
{
    public class PagingResponseModel
    {
        public int TotalCount { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public int TotalPages { get; set; } = 1;
        public int CurrentPage { get; set; } = 1;
        public int? NextPage { get; set; }
        public int? PreviousPage { get; set; }
    }
}
