using NewsWebSite.Domain.Entities.Commons;
using NewsWebSite.Domain.Entities.EntitiesNews;
using NewsWebSite.Domain.Entities.Users;

namespace NewsWebSite.Domain.Entities
{
    public class FileDocument
    {
        public long Id { get; set; }
        public Guid UniqId { get; set; }
        public byte[] Document { get; set; }
        public DateTime InsertTime { get; set; } = DateTime.Now;
        public DateTime? UpdateTime { get; set; }
    }
}
