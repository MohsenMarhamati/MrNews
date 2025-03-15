using Microsoft.AspNetCore.Http;

namespace NewsWebSite.Common.Dto.Users
{
    public class RequestRegisterUserDto
    {
        public long? Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public List<long>? Roles { get; set; }
        public string? RolesJson { get; set; }
        public string? Password { get; set; }
        public string? RePassword { get; set; }
        public IFormFile? File { get; set; }
    }
}
