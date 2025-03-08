using NewsWebSite.Common.Dto;
using NewsWebSite.Common.Dto.Like;

namespace NewsWebSite.Application.Services.Like
{
    public interface ILikeCommentService
    {
        public ResultDto AddLike(LikeCommentDto request);
        public ResultDto RemoveLike(LikeCommentDto request);
        public ResultDto ChengeLike(LikeCommentDto request);
    }
}
