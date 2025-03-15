using NewsWebSite.Common.Dto.Comment;

namespace NewsWebSite.Common.Dto.News
{
    public class GetNewsDto
    {
        public long Id { get; set; }
        public Decimal? Rating { get; set; }
        public Int16? UserRate { get; set; }
        public string? Title { get; set; }
        public string? Headline { get; set; }
        public string? InsertTime { get; set; }
        public Guid? DefaultImageId { get; set; }
        public long? CategoryId { get; set; }
        public string? CategoryTitle { get; set; }
        public int? LikeCount { get; set; }
    }
}
