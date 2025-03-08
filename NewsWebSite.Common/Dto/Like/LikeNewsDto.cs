using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsWebSite.Common.Dto.Like
{
    public class LikeNewsDto
    {
        public Decimal? Rate { get; set; }
        public Int16 UserRete { get; set; }
        public long? UserId { get; set; }
        public int? Count { get; set; }
        public long? NewsId { get; set; }
    }
}
