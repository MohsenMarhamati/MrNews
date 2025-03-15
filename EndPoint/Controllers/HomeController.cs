using Microsoft.AspNetCore.Mvc;
using NewsWebSite.Application.Services.Categories;
using NewsWebSite.Application.Services.Comment;
using NewsWebSite.Application.Services.Like;
using NewsWebSite.Application.Services.News;
using NewsWebSite.Common.Dto.Like;
using NewsWebSite.Common.Dto.News;
using System.Web;

namespace EndPoint.Controllers
{
    public class HomeController : Controller
    {
        INewsService _NewsService;
        ICategoriesService _CategoriesService;
        ICommentsService _CommentsService;
        ILikeNewsService _likeNewsService;
        public HomeController(INewsService news, ICategoriesService categories, ICommentsService comments, ILikeNewsService likeNews)
        {
            _NewsService = news;
            _CategoriesService = categories;
            _CommentsService = comments;
            _likeNewsService = likeNews;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SliderGet()
        {
            var request = new RequestSearchDto
            {
                Page = 1,
                PageSize = 3,
            };
            var result = _NewsService.SearchByMostTime(request);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult GetNews8()
        {
            var request = new RequestSearchDto
            {
                Page = 1,
                PageSize = 8,
            };
            var result = _NewsService.Get8LatestNews(request);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult LatestNews()
        {
            var request = new RequestSearchDto { 
            Page = 1,
            PageSize = 4,
            };

            var result = _NewsService.GetLatestNews(request);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult GetCategories()
        {
            var result = _CategoriesService.GetCategoriesForHomeLayout();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult LatesCamment()
        {
            var result = _CommentsService.GetCommentForHomeLayout();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult GetMostPopular(RequestSearchDto request)
        {
            request.UserId = Convert.ToInt64(HttpContext.Session.GetString("UserId"));
            var result = _NewsService.GetMostPopular(request);
            return Ok(result);
        }

        public IActionResult RateModal(GetNewsDto request)
        {
           return View(request);
        }

        [HttpPost]
        public IActionResult TopCategories()
        {
            var result = _NewsService.GetNewsinCategory();
            return Ok(result);
        }

        // ******************************* Service *******************************

        public IActionResult Service(string by)
        {
            return View(model: by);
        }


        public IActionResult SearchPeag(string q)
        {
            return View(model: q);
        }


        [HttpPost]
        public IActionResult GetNewsByModel(RequestSearchDto request)
        {
            request.Category = request.Category;
            request.SearchKey = HttpUtility.HtmlDecode(request.SearchKey);
            request.UserId = Convert.ToInt64(HttpContext.Session.GetString("UserId"));
            var result = _NewsService.GetNewsByModel(request);
            return Ok(result);
        }


        public IActionResult SetRate(LikeNewsDto request)
        {
            request.UserId = Convert.ToInt64(HttpContext.Session.GetString("UserId"));
            var result = _likeNewsService.AddLike(request);
            return Ok(result);
        }
    }
}