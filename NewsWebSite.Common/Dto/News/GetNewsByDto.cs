using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsWebSite.Common.Dto.News
{
    public class GetNewsByDto
    {
        public List<GetNewsDto>? List { get; set; } = new List<GetNewsDto >();
        public int? RecordCount { get; set; }
        public string? CategoryTitle { get; set; }
        public int? Rowe { get; set; }
    }
}
