using NewsWebSite.Common.Dto;
using NewsWebSite.Common.Dto.Comment;

namespace NewsWebSite.Application.Services.Comment
{
    public interface ICommentsService
    {
        public GetCommentsDto GetCommentsNews(CommentDto request);
        public ResultDto<List<CommentDto>> GetCommentForHomeLayout();
        public ResultDto SetCommentNews(CommentDto request);
    }
}
