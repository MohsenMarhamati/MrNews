namespace NewsWebSite.Common.Dto.Users
{
    public class RequestGetUserDto
    {
        public string? SearchKye { get; set; }
        public int page { get; set; } = 1;
        public int pagesize { get; set; } = 5;
    }
}

