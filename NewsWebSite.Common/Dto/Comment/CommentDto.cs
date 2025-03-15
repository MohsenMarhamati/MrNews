using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsWebSite.Common.Dto.Comment
{
    public class CommentDto
    {
        public long? Id { get; set; }
        public long? ParentId { get; set; }
        public long? RootId { get; set; }
        public string? Text { get; set; }
        public int? CountLike { get; set; }
        public int? CountDeslike { get; set; }
        public string? UserName { get; set; }
        public long? UserId { get; set; }
        public Guid? UserImage { get; set; }
        public long? NewsId { get; set; }
        public string? InsertTime { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public List<CommentDto>? RepComment { get; set; } = new List<CommentDto>();
    }
}
