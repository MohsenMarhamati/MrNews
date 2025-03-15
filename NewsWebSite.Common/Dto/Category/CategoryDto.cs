using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsWebSite.Common.Dto.Category
{
    public class CategoryDto
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int Number { get; set; }
        public Guid FileDocumentId { get; set; }
    }
}
