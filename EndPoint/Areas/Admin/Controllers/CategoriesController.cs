using Microsoft.AspNetCore.Mvc;
using NewsWebSite.Application.Services.Categories;
using NewsWebSite.Common.Attributes;
using NewsWebSite.Common.Dto.Category;

namespace EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CustomAuthorize("مدیر")]
    public class CategoriesController : Controller
    {
        private ICategoriesService _CategoriesService;
        public CategoriesController(ICategoriesService CategoriesService)
        {
            _CategoriesService = CategoriesService;
        }

        // ********** Index **********

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult GetCategories(int page = 1, int pagesize = 5)
        {
            var result = _CategoriesService.GetCategories(page, pagesize);
            return Ok(result);
        }


        [HttpPost]
        public IActionResult DeleteCategory(long id)
        {
            var result = _CategoriesService.RemoveCategory(id);
            return Ok(result);
        }


        [HttpPost]
        public IActionResult CategorySatusChange(long id)
        {
            var result = _CategoriesService.CategorySatusChenge(id);
            return Ok(result);
        }

        
        [HttpPost]
        public IActionResult EditCategory(RequestCategoryDto request)
        {
            var result = _CategoriesService.EditCategory(request);
            return Ok(result);
        }


        //Erorr
        [HttpPost]
        public IActionResult FindCategory(long id)
        {
            var result = _CategoriesService.FindCategory(id);
            return Ok(result);
        }
        

        // ********** Create **********

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(RequestCategoryDto Category)
        {
            var result = _CategoriesService.AddNewCategory(Category);
            return Json(result);
        }
    }
}
