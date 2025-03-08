namespace NewsWebSite.Domain.Entities.Users
{
    public class Role
        {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public ICollection<UserInRole>? UserInRoles { get; set; }

    }
}
