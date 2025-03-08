using NewsWebSite.Domain.Entities.Commons;
using NewsWebSite.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsWebSite.Domain.Entities.EntitiesNews
{
    public class News : BaseEntity
    {
        public string Title { get; set; }
        public string Headline { get; set; }
        public string Body { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? ActiveTime{ get; set; }
        public bool Read { get; set; } = false;
        public string? Reasons { get; set; }
        public Decimal? Rate { get; set; }
        public int RateCount { get; set; }
        public long CategoryId { get; set; }
        public Guid? DefaultImageId { get; set; }
        public virtual long? UserId { get; set; }
        public virtual User? User { get; set; }
        public virtual Category Category { get; set; }
        public ICollection<NewsImage>? NewsImages { get; set; }
        public ICollection<Comment>? Comment { get; set; }
        public ICollection<LikeNews>? LikeNews { get; set; }
    } 
}
