namespace NewsWebSite.Common.Dto.Users
{
    public class ReslutGetUserDto
    {
        public List<GetUserDto> UsersList { get; set; } = new List<GetUserDto>();
        public int RecordCount { get; set; }
        public int Rowe { get; set; }
    }
}

