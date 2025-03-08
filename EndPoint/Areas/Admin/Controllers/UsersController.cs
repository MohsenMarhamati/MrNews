using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsWebSite.Common.Dto.Users;
using NewsWebSite.Application.Services.Users;
using Newtonsoft.Json;
using NewsWebSite.Application.Services.Authentication;
using NewsWebSite.Common.Attributes;

namespace EndPoint.Site.Areas.Admin.Controllers;

[Area("Admin")]
[CustomAuthorize("مدیر")]
public class UsersController : Controller
{
    private readonly IUsersService _UsersService;
    public UsersController(IUsersService UsersService)
    {
        _UsersService = UsersService;
    }

    // ********** Index **********

    [HttpGet]
    public IActionResult Index(int page = 1, int pagesize = 5)
    {
        return View();
    }

    //[HttpGet]
    //public IActionResult Index(int page = 1, int pagesize = 5)
    //{
    //    if (Authentication.IsAdmin(HttpContext.Session.GetString("UserRole")))
    //    {
    //        return View();
    //    }
    //    return Redirect("/Home/Error");
    //}


    [HttpPost]
    public IActionResult GetUsers(int page = 1, int pagesize = 5)
    {
        var result = _UsersService.GetUsers(new RequestGetUserDto
        {
            page = page,
            pagesize = pagesize,
        });
        return Ok(result);
    }


    [HttpPost]
    public IActionResult DeleteUser(long id)
    {
        var result = _UsersService.RemoveUser(id);
        return Ok(result);
    }


    [HttpPost]
    public IActionResult UserSatusChange(long id)
    {
        var result = _UsersService.UserSatusChenge(id);
        return Ok(result);
    }


    [HttpPost]
    public IActionResult EditUser(RequestRegisterUserDto request)
    {
        request.Roles = JsonConvert.DeserializeObject<List<long>>(request.RolesJson);
        var result = _UsersService.AdminEditUser(request);
        return Ok(result);
    }


    [HttpPost]
    public IActionResult FindUser(long id)
    {
        var result = _UsersService.FindUser(id);
        return Ok(result);
    }



    // ********** Create **********

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public IActionResult Create(RequestRegisterUserDto request)
    {
        request.Roles = JsonConvert.DeserializeObject<List<long>>(request.RolesJson);
        var result = _UsersService.RegisterUser(request);
        return Json(result);
    }

    [HttpPost]
    public IActionResult GetRoles()
    {
        var result = _UsersService.GetRoles();
        return Json(result);
    }

}
