using NewsWebSite.Common.Dto.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsWebSite.Common.Dto.News
{
    public class GetPageNewsDto
    {
        public long? Id { get; set; }
        public string? Title { get; set; }
        public string? Headline { get; set; }
        public string? Body { get; set; }
        public string? InsertTime { get; set; }
        public Guid? DefaultImageId { get; set; }
        public List<Guid>? ImagesId { get; set; }
        public long? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryTitle { get; set; }
        public string? ReporterName { get; set; }
        public decimal? Rate { get; set; }
        public Int16? UserRate { get; set; }
        public int? LikeCount { get; set; }
        public List<CommentDto>? Comments { get; set; }
    }
}
