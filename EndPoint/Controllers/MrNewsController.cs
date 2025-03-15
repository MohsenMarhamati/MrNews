using Microsoft.AspNetCore.Mvc;
using NewsWebSite.Application.Services.Comment;
using NewsWebSite.Application.Services.Like;
using NewsWebSite.Application.Services.News;
using NewsWebSite.Common.Dto.Comment;
using NewsWebSite.Common.Dto.Like;

namespace EndPoint.Controllers
{
    public class MrNewsController : Controller
    {
        INewsService _NewsService;
        ICommentsService _CommentsService;
        ILikeNewsService _likeNewsService;
        public MrNewsController(INewsService news, ICommentsService comments, ILikeNewsService likeNews)
        {
            _NewsService = news;
            _CommentsService = comments;
            _likeNewsService = likeNews;
        }


        public IActionResult Page(long id)
        {
            return View(model : id);
        }

        [HttpPost]
        public IActionResult GetNews(long id)
        {
            var userid = Convert.ToInt64(HttpContext.Session.GetString("UserId"));
            var result = _NewsService.GetPage(id, userid);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult GetCamments(CommentDto request)
        {
            if(request.ParentId == 0) { request.ParentId = null; request.RootId = null;  }
            var result = _CommentsService.GetCommentsNews(request);
            return Ok(result);
        }
        
        [HttpPost]
        public IActionResult CreateCamments(CommentDto request)
        {
            request.UserId = Convert.ToInt64(HttpContext.Session.GetString("UserId"));
            var result = _CommentsService.SetCommentNews(request);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult SetRate(LikeNewsDto request)
        {
            request.UserId = Convert.ToInt64(HttpContext.Session.GetString("UserId"));
            var result = _likeNewsService.AddLike(request);
            return Ok(result);
        }
    }
}
