namespace NewsWebSite.Common.Dto.Users
{
    public class ResultUserloginDto
    {
        public long UserId { get; set; }
        public string Roles { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid? Image { get; set; }
    }
}
