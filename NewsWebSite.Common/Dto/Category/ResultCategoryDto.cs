using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsWebSite.Common.Dto.Category
{
    public class ResultCategoryDto
    {
        public List<CategoryDto>? Categories { get; set; }
        public int? RecordCount { get; set; }
        public int? Rowe { get; set; }
    }
}
