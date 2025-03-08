using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsWebSite.Common.Dto.News
{
    public class SetNewsDto
    {
        public long? Id { get; set; }
        public string? Title { get; set; }
        public string? Headline { get; set; }
        public string? Body { get; set; }
        public string? Reasons { get; set; }
        public bool? IsActive { get; set; }
        public long? CategoryId { get; set; }
        public long? UserId { get; set; }
        public Guid? DefaultImageId { get; set; }
        public List<Guid>? FileDocuments { get; set; }
    }
}
