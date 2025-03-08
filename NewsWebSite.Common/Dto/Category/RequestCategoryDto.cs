using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsWebSite.Common.Dto.Category
{
    public class RequestCategoryDto
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public IFormFile? File { get; set; }
    }
}
