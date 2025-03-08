using NewsWebSite.Domain.Entities.Users;

namespace NewsWebSite.Domain.Entities.EntitiesNews
{
    public class Comment
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public DateTime InsertTime { get; set; } = DateTime.Now;
        public long NewsId { get; set; }
        public long UserId { get; set; }
        public virtual News News { get; set; }
        public virtual User User { get; set; }
        public long? RootCommentId { get; set; }
        public long? ParentCommentId { get; set; }
        public virtual Comment ParentComment { get; set; }
        public ICollection<Comment> SubComments { get; set; } 
        public ICollection<LikeComment> LikeComment { get; set; }
    }
}
