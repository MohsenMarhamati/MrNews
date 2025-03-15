using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsWebSite.Common.Dto.News
{
    public class RequestSearchDto
    {
        public List<long>? Categories { get; set; }
        public string? Category { get; set; }
        public string? SearchKey { get; set; }
        public bool? IsRemove { get; set; }
        public bool? IsActive { get; set; }
        public long? UserId { get; set; }
        public int? Filter { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public int Count { get; set; }
    }
}
