using NewsWebSite.Application.Interfaces.Context;
using NewsWebSite.Common.Dto;
using NewsWebSite.Common.Dto.Like;
using NewsWebSite.Domain.Entities.EntitiesNews;

namespace NewsWebSite.Application.Services.Like
{
    public class LikeCommentService : ILikeCommentService
    {
        private IDataBaseContext _Context;
        public LikeCommentService(IDataBaseContext Cantext)
        {
            _Context = Cantext;
        }

        #region AddLikeService
        public ResultDto AddLike(LikeCommentDto request)
        {
            try
            {
                if (request == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = ""
                    };
                }

                if (_Context.LikeComment.Any(l => l.CommentId == request.CommentId && l.UserId == request.UserId))
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = ""
                    };
                }

                var like = new LikeComment
                {
                    UserId = request.UserId,
                    CommentId = request.CommentId,
                    LikeOrDeslike = request.LikeOrDeslike,
                };

                _Context.LikeComment.Add(like);
                _Context.savechanges();

                var result = new ResultDto
                {
                    IsSuccess = true,
                    Message = ""
                };
                return result;
            }
            catch (Exception)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = ""
                };
            }
        }
        #endregion


        #region RemoveLikeService
        public ResultDto RemoveLike(LikeCommentDto request)
        {
            try
            {
                if (request == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = ""
                    };
                }

                var like = new LikeComment
                {
                    UserId = request.UserId,
                    CommentId = request.CommentId,
                    LikeOrDeslike = request.LikeOrDeslike,
                };

                _Context.LikeComment.Remove(like);
                _Context.savechanges();

                var result = new ResultDto
                {
                    IsSuccess = true,
                    Message = "",
                };

                return result;
            }
            catch (Exception)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = ""
                };
            }
        }
        #endregion


        #region ChengeLikeService
        public ResultDto ChengeLike(LikeCommentDto request)
        {
            try
            {
                if (request == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = ""
                    };
                }

                var like = _Context.LikeComment.Where(l => l.CommentId == request.CommentId && l.UserId == request.UserId).First();
                like.LikeOrDeslike = !like.LikeOrDeslike;
                _Context.savechanges();

                var resulet = new ResultDto
                {
                    IsSuccess = true,
                    Message = ""
                };

                return resulet;
            }
            catch
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = ""
                };
            }
        }
        #endregion
    }
}
