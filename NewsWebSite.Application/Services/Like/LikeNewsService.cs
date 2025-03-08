using NewsWebSite.Application.Interfaces.Context;
using NewsWebSite.Common.Dto;
using NewsWebSite.Common.Dto.Like;
using NewsWebSite.Domain.Entities.EntitiesNews;

namespace NewsWebSite.Application.Services.Like
{
    public class LikeNewsService : ILikeNewsService
    {
        private IDataBaseContext _Context;
        public LikeNewsService(IDataBaseContext Cantext)
        {
            _Context = Cantext;
        }

        #region AddLikeService
        public ResultDto<LikeNewsDto> AddLike(LikeNewsDto request)
        {
            try
            {
                if (request == null || request.UserRete == null)
                {
                    return new ResultDto<LikeNewsDto>
                    {
                        IsSuccess = false,
                        Message = "در ارسال داده به سرور مشکلی پیش آمده است"
                    };
                }

                if (!_Context.News.Any(n => n.Id == request.NewsId))
                {
                    return new ResultDto<LikeNewsDto>
                    {
                        IsSuccess = false,
                        Message = "خبر یافت نشد"
                    };
                }

                if (request.UserId == 0 || request.UserId == null)
                {
                    return new ResultDto<LikeNewsDto>
                    {
                        IsSuccess = false,
                        Message = "برای ثبت خبر ابتدا باید به حساب کاربری خود وارد شوید"
                    };
                }

                if (request.UserRete > 5) { request.UserRete = 5; }
                if (request.UserRete < 1) { request.UserRete = 1; }



                var oldLike = _Context.LikeNews.FirstOrDefault(l => l.NewsId == request.NewsId && l.UserId == request.UserId);
                
                if (oldLike == null)//قبلا رای نداده
                {
                    var like = new LikeNews
                    {
                        Rate = Convert.ToInt16(request.UserRete),
                        UserId = request.UserId.Value,
                        NewsId = request.NewsId.Value,
                    };
                    _Context.LikeNews.Add(like);
                }
                else
                {
                    oldLike.Rate = request.UserRete;
                }
                _Context.savechanges();

                var news = _Context.News.FirstOrDefault(n => n.Id == request.NewsId);
                var sumRate = _Context.LikeNews.Where(l => l.NewsId == request.NewsId).Sum(l => l.Rate);
                var count = _Context.LikeNews.Where(l => l.NewsId == request.NewsId).Count();
                news.Rate = Convert.ToDecimal(sumRate) / count;
                
                _Context.savechanges();
                var result = new ResultDto<LikeNewsDto>
                {
                    IsSuccess = true,
                    Message = "امتیاز با موفقیت ثبت شد.",
                    Data = new LikeNewsDto { Rate = Math.Round(news.Rate ?? 0, 1), Count = count }//امتیاز جدید
                };
                return result;
            }
            catch(Exception e)
            {
                return new ResultDto<LikeNewsDto>
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }
        }
        #endregion


    }
}
