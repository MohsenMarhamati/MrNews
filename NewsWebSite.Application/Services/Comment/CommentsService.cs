using Microsoft.EntityFrameworkCore;
using NewsWebSite.Application.Interfaces.Context;
using NewsWebSite.Common;
using NewsWebSite.Common.Dto;
using NewsWebSite.Common.Dto.Comment;

namespace NewsWebSite.Application.Services.Comment
{
    public class CommentsService : ICommentsService
    {
        private IDataBaseContext _Cantext;
        public CommentsService(IDataBaseContext context)
        {
            _Cantext = context;
        }

        #region GetCommentsNewsService
        public GetCommentsDto GetCommentsNews(CommentDto request)
        {
            try
            
            {
                var rpweCount = 0;
                var comments = _Cantext.Comment
                    .Where(c => c.NewsId == request.Id && c.RootCommentId == null)
                    //.Include(c => c.LikeComment)
                    .Include(c => c.User)
                    .ToPaged(request.Page.Value, 5, out rpweCount)
                    .OrderByDescending(n => n.InsertTime)
                    .Select(c => new CommentDto
                    {
                        Id = c.Id,
                        ParentId = c.ParentCommentId,
                        Text = c.Text,
                        //CountLike = c.LikeComment.Where(l => l.LikeOrDeslike == true).Count(),
                        //CountDeslike = c.LikeComment.Where(l => l.LikeOrDeslike == false).Count(),
                        InsertTime = DateUtility.PersianDateTime(c.InsertTime),
                        UserName = c.User.FullName,
                        UserImage = c.User.FileDocumentId,
                        RepComment = _Cantext.Comment
                            .Where(re => re.RootCommentId == c.Id)
                            .Select(re => new CommentDto
                            {
                                Id = re.Id,
                                ParentId = re.ParentCommentId,
                                Text = re.Text,
                                CountLike = re.LikeComment.Where(l => l.LikeOrDeslike == true).Count(),
                                CountDeslike = re.LikeComment.Where(l => l.LikeOrDeslike == false).Count(),
                                InsertTime = DateUtility.PersianDateTime(re.InsertTime),
                                UserName = c.User.FullName,
                                UserImage = c.User.FileDocumentId,
                            }).ToList(),
                    }).ToList();

                var result = new GetCommentsDto
                {
                    List = comments,
                    RecordCount = rpweCount,
                    Rowe = rpweCount,
                };

                return result;
            }
            catch
            {
                return null;
            }
        }
        #endregion


        #region GetCommentForHomeLayoutService
        public ResultDto<List<CommentDto>> GetCommentForHomeLayout()
        {
            try
            {
                var comments = _Cantext.Comment
                    .Where(c => c.RootCommentId == null)
                    .Include(c => c.News)
                    //.Include(c => c.LikeComment)
                    .Include(c => c.User)
                    .OrderByDescending(n => n.InsertTime)
                    .Take(10)
                    .Select(c => new CommentDto
                    {
                        NewsId = c.NewsId,
                        Text = c.Text,
                        //CountLike = c.LikeComment.Where(l => l.LikeOrDeslike == true).Count(),
                        //CountDeslike = c.LikeComment.Where(l => l.LikeOrDeslike == false).Count(),
                        InsertTime = DateUtility.PersianDateTime(c.InsertTime),
                        UserName = c.User.FullName,
                        UserImage = c.User.FileDocumentId,
                    }).ToList();

                var result = new ResultDto<List<CommentDto>>
                {
                    Data = comments,
                    IsSuccess = true,
                    Message = ""
                };

                return result;
            }
            catch
            {
                return null;
            }
        }
        #endregion


        #region SetCommentNewsService
        public ResultDto SetCommentNews(CommentDto request)
        {
            try
            {
                if (request == null)
                {
                    return new ResultDto { IsSuccess = false, Message = "در انتقال داده مشکلی پیش آمده است" };
                }

                if (request.NewsId == null)
                {
                    return new ResultDto { IsSuccess = false, Message = "خبر یافت نشد" };
                }

                if (request.UserId == 0 || request.UserId == null)
                {
                    return new ResultDto { IsSuccess = false, Message = "برای ثبت نظر باید به حساب کاربری خود وارد شوید" };
                }

                if (request.Text == null)
                {
                    return new ResultDto { IsSuccess = false, Message = "متن نظر را وارد کنید" };
                }



                var comment = new Domain.Entities.EntitiesNews.Comment
                {
                    Text = request.Text,
                    NewsId = request.NewsId.Value,
                    UserId = request.UserId.Value,
                    ParentCommentId = request.ParentId,
                    RootCommentId = request.RootId ?? request.ParentId,
                };

                _Cantext.Comment.Add(comment);
                _Cantext.savechanges();

                var result = new ResultDto
                {
                    IsSuccess = true,
                    Message = "نظر شما با موفقیت ثبت شد"
                };

                return result;
            }
            catch
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "خطایی رخ داده است"
                };
            }
        }
        #endregion
    }
}
