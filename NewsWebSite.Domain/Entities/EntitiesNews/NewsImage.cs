using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsWebSite.Domain.Entities.EntitiesNews
{
    public class NewsImage
    {
        public int Id { get; set; }
        public long NewsId { get; set; }
        public Guid FileDocumentId { get; set; }
        public virtual News News { get; set; }
    }
}
