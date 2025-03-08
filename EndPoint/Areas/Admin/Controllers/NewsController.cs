using Microsoft.AspNetCore.Mvc;
using NewsWebSite.Application.Services.Authentication;
using NewsWebSite.Application.Services.News;
using NewsWebSite.Common.Attributes;
using NewsWebSite.Common.Dto.News;

namespace EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CustomAuthorize("مدیر")]
    public class NewsController : Controller
    {
        private INewsService _NewsService;

        public NewsController(INewsService newsService)
        {
            _NewsService = newsService;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult GetNews(RequestSearchDto request)
        {
            var result = _NewsService.GetNewsView(new RequestSearchDto
            {
                Page = request.Page,
                PageSize = request.PageSize,
                Filter = request.Filter,
            });
            return Ok(result);
        }


        [HttpPost]
        public IActionResult ReadNews(long id)
        {
            var result = _NewsService.GetPage(id, 0);
            if(result.IsSuccess == true)
            { 
                var read = _NewsService.ReadNews(id);
                if (read.IsSuccess == false)
                {
                    result.IsSuccess = read.IsSuccess;
                    result.Message = read.Message;
                }
            }
            return Ok(result);
        }


        [HttpPost]
        public IActionResult Delete(long id)
        {
            var user = HttpContext.UserInfo();
            if(user.UserId == 1)
            {

            }
            var result = _NewsService.RemoveNews(id);
            return Ok(result);
        }


        [HttpPost]
        public IActionResult SatusChange(SetNewsDto request)
        {
            var result = _NewsService.NewsSatusChenge(request);
            return Ok(result);
        }
    }
}
