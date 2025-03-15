using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsWebSite.Application.Services.Categories;
using NewsWebSite.Application.Services.News;
using NewsWebSite.Application.Services.Users;
using NewsWebSite.Common.Attributes;
using NewsWebSite.Common.Dto.News;

namespace EndPoint.Controllers
{
    [Area("Reporter")]
    [CustomAuthorize("خبرنگار")]
    public class NewsController : Controller
    {
        public INewsService _NewsService;
        public IUsersService _UsersService;
        public ICategoriesService _CategoriesService;
        public NewsController(INewsService news, IUsersService users, ICategoriesService categories)
        {
            _NewsService = news;
            _UsersService = users;
            _CategoriesService = categories;
        }


        // ********** Index **********
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetMyNews(RequestSearchDto request)
        {
            var result = _NewsService.GetNewsView(new RequestSearchDto
            {
                Page = request.Page,
                PageSize = request.PageSize,
                Filter = request.Filter,
                UserId = Convert.ToInt64(HttpContext.Session.GetString("UserId")),
            });
            return Ok(result);
        }

        [HttpPost]
        public IActionResult GetCategories(long id)
        {
            var result = _CategoriesService.GetCategories(1,250);
            return Ok(result);
        }



        [HttpPost]
        public IActionResult FilterNews(RequestSearchDto request)
        {
            var result = _NewsService.GetNewsView(request);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            var result = _NewsService.RemoveNews(id);
            return Ok(result);
        }
        
        [HttpPost]
        public IActionResult ShowEditModal(long id)
        {
            var result = _NewsService.GetPage(id, 0);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult EditNews(SetNewsDto request)
        {
            request.UserId = Convert.ToInt64(HttpContext.Session.GetString("UserId"));
            var result = _NewsService.EditNews(request);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult ReadNews(long id)
        {
            var result = _NewsService.GetPage(id, 0);
            return Ok(result);
        }


        // ********** Create **********
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_CategoriesService.GetNewsCategories().Data, "Id", "Title");
                return View();
        }

        [HttpPost]
        public IActionResult Create(SetNewsDto request)
        {
            request.UserId = Convert.ToInt64(HttpContext.Session.GetString("UserId"));
            var results = _NewsService.SetNews(request);
            return Ok(results);
        }
    }
}
