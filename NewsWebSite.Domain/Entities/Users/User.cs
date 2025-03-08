using NewsWebSite.Domain.Entities.Commons;
using NewsWebSite.Domain.Entities.EntitiesNews;

namespace NewsWebSite.Domain.Entities.Users
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid? FileDocumentId { get; set; }
        public ICollection<News> News { get; set; }
        public ICollection<UserInRole> UserInRoles { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
