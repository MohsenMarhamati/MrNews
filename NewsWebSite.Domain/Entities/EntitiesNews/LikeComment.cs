using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsWebSite.Domain.Entities.EntitiesNews
{
    public class LikeComment
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public bool LikeOrDeslike { get; set; }
        public long CommentId { get; set; }
        public virtual Comment Comment { get; set; }
    }
}
