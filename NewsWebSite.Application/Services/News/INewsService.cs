using NewsWebSite.Common.Dto;
using NewsWebSite.Common.Dto.News;

namespace NewsWebSite.Application.Services.News
{
    public interface INewsService
    {
        public ResultDto<List<GetNewsDto>> GetLatestNews(RequestSearchDto request);
        public ResultDto<GetPageNewsDto> GetPage(long id, long UserId);
        public ResultGetNewsForTable GetNewsView(RequestSearchDto request);
        public List<GetNewsByDto> GetNewsinCategory();
        public ResultDto SetNews(SetNewsDto request);
        public ResultDto EditNews(SetNewsDto request);
        public ResultDto<GetNewsByDto> GetNewsByModel(RequestSearchDto request);
        public ResultDto<List<GetNewsDto>> GetMostPopular(RequestSearchDto request);
        public ResultDto<List<GetNewsDto>> Get8LatestNews(RequestSearchDto request);
        public ResultDto<List<GetNewsDto>> SearchByMostTime(RequestSearchDto request);
        public ResultDto NewsSatusChenge(SetNewsDto request);
        public ResultDto RemoveNews(long id);
        public ResultDto ReadNews(long id);
    }
}
