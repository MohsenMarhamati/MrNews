using Microsoft.EntityFrameworkCore;
using NewsWebSite.Application.Interfaces.Context;
using NewsWebSite.Common;
using NewsWebSite.Common.Dto;
using NewsWebSite.Common.Dto.News;
using NewsWebSite.Domain.Entities.EntitiesNews;

namespace NewsWebSite.Application.Services.News
{
    public class NewsService : INewsService
    {
        private IDataBaseContext _Context;

        public NewsService(IDataBaseContext context)
        {
            _Context = context;
        }

        #region GetLatestNewsSkip4Service
        public ResultDto<List<GetNewsDto>> GetLatestNews(RequestSearchDto request)
        {
            try
            {
                var rpweCount = 0;
                var News = _Context.News
                    .Include(n => n.Category)
                    .Include(n => n.LikeNews)
                    .Where(n => n.IsRemoved == false && n.IsActive == true)
                    .OrderByDescending(n => n.ActiveTime)
                    .Skip(4)
                    .ToPaged(request.Page, request.PageSize, out rpweCount)
                    .Select(n => new GetNewsDto
                    {
                        Id = n.Id,
                        Title = n.Title
                    }).ToList();

                var result = new ResultDto<List<GetNewsDto>>
                {
                    Data = News,
                    IsSuccess = true,
                    Message = ""
                };

                return result;
            }
            catch (Exception)
            {
                return new ResultDto<List<GetNewsDto>>
                {
                    IsSuccess = false,
                    Message = "خطایی پیش آمده است"
                };
            }
        }
        #endregion


        #region GetLatestNewsSkip8Service
        public ResultDto<List<GetNewsDto>> Get8LatestNews(RequestSearchDto request)
        {
            try
            {
                var rpweCount = 0;
                var News = _Context.News
                    .Where(n => n.IsRemoved == false && n.IsActive == true && n.DefaultImageId != null)
                    .OrderByDescending(n => n.ActiveTime)
                    .Skip(8)
                    .ToPaged(request.Page, request.PageSize, out rpweCount)
                    .Select(n => new GetNewsDto
                    {
                        Id = n.Id,
                        Title = n.Title,
                        DefaultImageId = n.DefaultImageId,
                    }).ToList();

                var result = new ResultDto<List<GetNewsDto>>
                {
                    Data = News,
                    IsSuccess = true,
                    Message = ""
                };

                return result;
            }
            catch (Exception)
            {
                return new ResultDto<List<GetNewsDto>>
                {
                    IsSuccess = false,
                    Message = "خطایی پیش آمده است"
                };
            }
        }
        #endregion


        #region GetPageService
        public ResultDto<GetPageNewsDto> GetPage(long id, long UserId)
        {
            try
            {
                var News = _Context.News
                    .Where(n => n.Id == id)
                    .Include(n => n.Category)
                    .Include(n => n.LikeNews)
                    .Include(n => n.NewsImages)
                    .Select(n => new GetPageNewsDto
                    {
                        Id = id,
                        Title = n.Title,
                        Headline = n.Headline,
                        Body = n.Body,
                        InsertTime = DateUtility.PersianDateTime(n.ActiveTime) ?? DateUtility.PersianDateTime(n.InsertTime),
                        CategoryId = n.CategoryId,
                        CategoryName = n.Category.Name,
                        CategoryTitle = n.Category.Title,
                        ReporterName = n.User.FullName,
                        LikeCount = n.LikeNews.Count(),
                        Rate = Math.Round(n.Rate ?? 0, 1),
                        UserRate = n.LikeNews.Where(l => l.UserId == UserId && l.NewsId == id).FirstOrDefault().Rate,
                        DefaultImageId = n.DefaultImageId,
                        ImagesId = n.NewsImages.Select(img => img.FileDocumentId).ToList(),
                    }).First();

                if (News == null)
                {
                    return new ResultDto<GetPageNewsDto>
                    {
                        IsSuccess = false,
                        Message = "خبر مورد نظر یافت نشد"
                    };
                }

                var result = new ResultDto<GetPageNewsDto>
                {
                    Data = News,
                    IsSuccess = true,
                    Message = ""
                };

                return result;
            }
            catch
            {
                return new ResultDto<GetPageNewsDto>
                {
                    IsSuccess = false,
                    Message = "خطایی پیش آمده است"
                };
            }
        }
        #endregion


        #region GetNewsinCategoryService
        public List<GetNewsByDto> GetNewsinCategory()
        {
            try
            {
                // SQL : 👇👇
                //; with cte_categories as (
                //SELECT top(4)
                //c.Id,c.Name
                //FROM Categories c
                //order by(select Count(n.Id) from News n where n.CategoryId = c.Id and n.IsActive = 1 and n.IsRemoved = 0) DESC
                //)
                //select* from cte_categories c
                //join News n on n.CategoryId = c.Id
                //where
                //n.IsActive = 1
                //and n.IsRemoved = 0


                var Result = _Context.Categories
                    .Include(c => c.News)
                    .OrderByDescending(c => c.News.Where(n => !n.IsRemoved && n.IsActive == true).Count())
                    .Take(4)
                    .Select(c =>
                        new GetNewsByDto()
                        {
                            CategoryTitle = c.Title,
                            List = c.News.Where(n => !n.IsRemoved && n.IsActive == true).OrderByDescending(n => n.ActiveTime).Take(4).Select( n => new GetNewsDto
                            {
                                Id = n.Id,
                                Title = n.Title,
                                Headline = n.Headline,
                                DefaultImageId = n.DefaultImageId,
                                InsertTime = DateUtility.PersianDateTime(n.ActiveTime) ?? DateUtility.PersianDateTime(n.InsertTime),
                            }).ToList(),
                        })
                    .ToList();

                return Result;
            }
            catch
            {
                return null;
            }
        }
        #endregion


        #region GetNewsViewService
        public ResultGetNewsForTable GetNewsView(RequestSearchDto request)
        {
            try
            {
                var rpweCount = 0;
                var News = _Context.News
                    .Include(n => n.Category)
                    .Include(n => n.User)
                    .Where(n => (request.UserId == null || n.UserId == request.UserId) && n.IsRemoved == false)
                    .Where(n => (n.IsActive == true && request.Filter == 1) || (n.IsActive == null && request.Filter == 2) ||
                    (n.IsActive == false && request.Filter == 3) || request.Filter == null) 
                    .OrderByDescending(n => n.UpdateTime ?? n.InsertTime)
                    .Select(n => new GetNewsView
                    {
                        Id = n.Id,
                        Title = n.Title,
                        CategoryId = n.CategoryId,
                        CategoryTitle = n.Category.Title,
                        DefaultImageId = n.DefaultImageId,
                        InsertTime = DateUtility.PersianDateTime(n.ActiveTime) ?? DateUtility.PersianDateTime(n.InsertTime),
                        IsActive = n.IsActive,
                        Reasons = n.Reasons,
                        Read = n.Read,
                        IsRemove = n.IsRemoved,
                        ReporterName = n.User.FullName,
                        ReporterEmail = n.User.Email,
                    }).ToPaged(request.Page, request.PageSize, out rpweCount)
                    .ToList();

                var result = new ResultGetNewsForTable
                {
                    Data = News,
                    RecordCount = rpweCount,
                    Rowe = rpweCount,
                };

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion


        #region GetNewsByModelService
        public ResultDto<GetNewsByDto> GetNewsByModel(RequestSearchDto request)
        {
            try
            {
                var rpweCount = 0;
                var News = _Context.News
                .Include(n => n.Category)
                .Include(n => n.LikeNews)
                .Where(n => n.IsRemoved == false && n.IsActive == true)
                .Where(n => n.Category.Name == request.Category || request.Category == null)
                .Where(n => (n.Title.Contains(request.SearchKey) || n.Body.Contains(request.SearchKey)
                        || n.Headline.Contains(request.SearchKey)) || request.SearchKey == null)
                .OrderByDescending(n => n.InsertTime)
                .ToPaged(request.Page, request.PageSize, out rpweCount)
                .Select(n => new GetNewsDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    Headline = n.Headline,
                    DefaultImageId = n.DefaultImageId,
                    InsertTime = DateUtility.PersianDateTime(n.ActiveTime) ?? DateUtility.PersianDateTime(n.InsertTime),
                    CategoryTitle = n.Category.Title,
                    LikeCount = n.LikeNews.Count(),
                    Rating = Math.Round(n.Rate ?? 0, 1),
                    UserRate = n.LikeNews.Where(l => l.UserId == request.UserId).FirstOrDefault()?.Rate
                }).ToList();

                var count = rpweCount;

                if (News == null && News.Count == 0)
                {
                    return new ResultDto<GetNewsByDto>
                    {
                        IsSuccess = false,
                        Message = "خبری مربوط به گروه خبری درخواستی یافت نشد"
                    };
                }

                var data = new GetNewsByDto
                {
                    List = News,
                    RecordCount = count,
                    Rowe = rpweCount
                };

                var result = new ResultDto<GetNewsByDto>
                {
                    Data = data,
                    IsSuccess = true,
                    Message = ""
                };

                return result;

            }
            catch (Exception)
            {
                return new ResultDto<GetNewsByDto>
                {
                    IsSuccess = false,
                    Message = "خطایی پیش آمده است"
                };
            }
        }
        #endregion


        #region GetMostPopularService
        public ResultDto<List<GetNewsDto>> GetMostPopular(RequestSearchDto request)
        {
            try
            {
                var News = _Context.News
                    .Include(n => n.Category)
                    .Include(n => n.LikeNews)
                    .Where(n => n.IsRemoved == false && n.IsActive == true)
                    .OrderByDescending(n => n.RateCount)
                    .ThenByDescending(n => n.Rate)
                    .Take(10)
                    .Select(n => new GetNewsDto
                    {
                        Id = n.Id,
                        Title = n.Title,
                        DefaultImageId = n.DefaultImageId,
                        InsertTime = DateUtility.PersianDateTime(n.ActiveTime) ?? DateUtility.PersianDateTime(n.InsertTime),
                        LikeCount = n.LikeNews.Count(),
                        Rating = Math.Round(n.Rate ?? 0, 1),
                        UserRate = n.LikeNews.Where(l => l.UserId == request.UserId && l.NewsId == n.Id).First().Rate,
                    }).ToList();

                var result = new ResultDto<List<GetNewsDto>>
                {
                    Data = News,
                    IsSuccess = true,
                    Message = ""
                };

                return result;
            }
            catch (Exception)
            {
                return new ResultDto<List<GetNewsDto>>
                {
                    IsSuccess = false,
                    Message = "خطایی پیش آمده است"
                };
            }
        }
        #endregion


        #region SearchByMostTimeService
        public ResultDto<List<GetNewsDto>> SearchByMostTime(RequestSearchDto request)
        {
            try
            {
                var rpweCount = 0;
                var News = _Context.News
                    .Where(n => n.IsRemoved == false && n.IsActive == true && n.DefaultImageId != null)
                    .OrderByDescending(n => n.ActiveTime)
                    .ToPaged(request.Page, request.PageSize, out rpweCount)
                    .Select(n => new GetNewsDto
                    {
                        Id = n.Id,
                        Title = n.Title,
                        DefaultImageId = n.DefaultImageId,
                        InsertTime = DateUtility.PersianDateTime(n.ActiveTime) ?? DateUtility.PersianDateTime(n.InsertTime),
                    }).ToList();

                var result = new ResultDto<List<GetNewsDto>>
                {
                    Data = News,
                    IsSuccess = true,
                    Message = ""
                };

                return result;
            }
            catch (Exception)
            {
                return new ResultDto<List<GetNewsDto>>
                {
                    IsSuccess = false,
                    Message = "خطایی پیش آمده است"
                };
            }
        }
        #endregion


        #region setNewsService
        public ResultDto SetNews(SetNewsDto request)
        {
            try
            {
                if (request == null)
                {
                    return new ResultDto { IsSuccess = false, Message = "در ارسال اطلاعات مشکی پیش آمده" };
                }

                if (request.Headline == null && request.Title == null && request.Body == null)
                {
                    return new ResultDto { IsSuccess = false, Message = "تمامی موارد خواسته شده را وارد کنید" };
                }

                if (request.CategoryId == null)
                {
                    return new ResultDto { IsSuccess = false, Message = "گروه خبری را برای خبر خود انتخاب کنید" };
                }

                if (request.DefaultImageId == null && request.FileDocuments != null)
                {
                    if (request.FileDocuments.Count() == 1) { request.DefaultImageId = request.FileDocuments[0]; }
                    else return new ResultDto { IsSuccess = false, Message = "عکسی که مایلید نمایه خبر شما باشد را انتخاب کنید" };
                }

                var news = new Domain.Entities.EntitiesNews.News
                {
                    Title = request.Title,
                    Body = request.Body,
                    Headline = request.Headline,
                    CategoryId = request.CategoryId.Value,
                    UserId = request.UserId.Value,
                    DefaultImageId = request.DefaultImageId,
                    InsertTime = DateTime.Now,
                };

                _Context.News.Add(news);
                _Context.savechanges();

                if (request.FileDocuments != null)
                {
                    for (var i = 0; i < request.FileDocuments.Count(); i++)
                    {
                        _Context.NewsImage.Add(new NewsImage
                        {
                            NewsId = news.Id,
                            FileDocumentId = request.FileDocuments[i],
                        });

                    }
                    _Context.savechanges();
                }

                var result = new ResultDto
                {
                    IsSuccess = true,
                    Message = "خبر با موفقیت بارگزاری شد"
                };

                return result;
            }
            catch
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "خطایی پیش آمده است"
                };
            }
        }
        #endregion


        #region EditNewsService
        public ResultDto EditNews(SetNewsDto request)
        {
            try
            {
                var news = _Context.News.Where(n => n.Id == request.Id && n.IsRemoved == false && n.UserId == request.UserId).Include(n => n.NewsImages).First();

                if (news == null) { return new ResultDto { IsSuccess = false, Message = "خبر یافت نشد" }; }

                if (news.Read && news.IsActive == null) { return new ResultDto { IsSuccess = false, Message = "خبر توسط مدیر سایت رویت شده است و امکان تغیر آن دیگر وجود ندارد" }; }

                if (news.IsActive == true) { return new ResultDto { IsSuccess = false, Message = "نمی توان در خبر فعال تغییری داد" }; }

                if (request == null) { return new ResultDto { IsSuccess = false, Message = "در ارسال اطلاعات مشکی پیش آمده" }; }

                if (request.DefaultImageId == null && request.FileDocuments != null)
                {
                    if (request.FileDocuments.Count() == 1) { request.DefaultImageId = request.FileDocuments[0]; }
                    else return new ResultDto { IsSuccess = false, Message = "عکسی که مایلید نمایه خبر شما باشد را انتخاب کنید" };
                }


                if (request.Title != null && request.Title != news.Title) { news.Title = request.Title; }
                if (request.Headline != null && request.Headline != news.Headline) { news.Headline = request.Headline; }
                if (request.Body != null && request.Body != news.Body) { news.Body = request.Body; }
                if (request.CategoryId != null && request.CategoryId != news.CategoryId) { news.CategoryId = request.CategoryId.Value; }
                if (request.DefaultImageId != null && request.DefaultImageId != news.DefaultImageId) { news.DefaultImageId = request.DefaultImageId; }
                var Images = new List<NewsImage>();
                for (var i = 0; i < request.FileDocuments.Count(); i++)
                {
                    Images.Add(new NewsImage
                    {
                        NewsId = news.Id,
                        FileDocumentId = request.FileDocuments[i],
                    });
                }

                news.Read = false;
                news.IsActive = null;
                news.NewsImages = Images;
                news.UpdateTime = DateTime.Now;
                _Context.savechanges();

                var result = new ResultDto
                {
                    IsSuccess = true,
                    Message = "خبر با موفقیت اصلاح شد"
                };

                return result;
            }
            catch
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "خطایی پیش آمده است"
                };
            }
        }
        #endregion


        #region NewsSatusChengeService
        public ResultDto NewsSatusChenge(SetNewsDto request)
        {
            try
            {
                var news = _Context.News.Find(request.Id);

                if (news == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "خبر یافت نشد"
                    };
                }

                if (request.IsActive == false && request.Reasons == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "ذکر دلایل رد خبر اجباری می باشد"
                    };
                }

                news.IsActive = request.IsActive;
                news.ActiveTime = request.IsActive == true ? DateTime.Now : null;
                news.Reasons = request.Reasons;
                _Context.savechanges();

                string IsActive = news.IsActive == true ? "فعال" : "رد";
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = $"خبر با موفقیت {IsActive} شد!"
                };
            }
            catch (Exception)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = $"مشکلی پیش آمده است"
                };
            }
        }
        #endregion


        #region RemoveNewsService
        public ResultDto RemoveNews(long id)
        {
            try
            {
                var news = _Context.News.Find(id);
                if (news == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "خبر یافت نشد",
                    };
                }

                news.RemoveTime = DateTime.Now;
                news.IsRemoved = true;
                _Context.savechanges();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "خبر با موفقیت حذف شد",
                };
            }
            catch (Exception)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "حذف خبر ناموفق بود",
                };
            }
        }
        #endregion


        #region ReadNewsService
        public ResultDto ReadNews(long id)
        {
            try
            {
                //var news = _Context.News.Find(id);
                var news = _Context.News.Where(n => n.Id == id).First();
                if (news == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "خبر یافت نشد",
                    };
                }

                news.Read = true;
                _Context.savechanges();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "",
                };
            }
            catch (Exception)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "بارگزاری خبر ناموفق بود",
                };
            }
        }
        #endregion
    }
}  