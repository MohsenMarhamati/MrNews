using NewsWebSite.Domain.Entities.Commons;

namespace NewsWebSite.Domain.Entities.EntitiesNews
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid FileDocumentId { get; set; }
        public ICollection<News> News { get; set; }
    }
}
