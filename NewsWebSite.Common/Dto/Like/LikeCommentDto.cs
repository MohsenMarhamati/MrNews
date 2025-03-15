namespace NewsWebSite.Common.Dto.Like
{
    public class LikeCommentDto
    {
        public long UserId { get; set; }
        public long CommentId { get; set; }
        public bool LikeOrDeslike { get; set; }
    }
}
