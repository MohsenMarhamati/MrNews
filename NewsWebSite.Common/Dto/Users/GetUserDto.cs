namespace NewsWebSite.Common.Dto.Users
{
    public class GetUserDto
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsRemoved { get; set; }
        public bool IsActive { get; set; }
        public Guid? FileDocumentId { get; set; }
        public List<RolesDto>  UserRoles { get; set; }
    }
}

