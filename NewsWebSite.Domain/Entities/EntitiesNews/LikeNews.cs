using NewsWebSite.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsWebSite.Domain.Entities.EntitiesNews
{
    public class LikeNews
    {
        public long Id { get; set; }
        public Int16 Rate { get; set; }
        public long NewsId { get; set; }
        public virtual News News { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}
