using EndPoint.Site.Models.ViewModels.AuthenticationViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NewsWebSite.Application.Services.Users;
using NewsWebSite.Common.Attributes;
using NewsWebSite.Common.Dto.Users;

namespace EndPoint.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUsersService _UsersService;
        public AuthenticationController(IUsersService UsersService) 
        {
            _UsersService = UsersService;
        }

        //***********Signup***********
        public IActionResult Signup(string ReturnUrl = "/")
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return View("/Viwe/Shared/Error.cshtml");
            }
            ViewBag.url = ReturnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Signup(SignupViewModel request)
        {
            var signeupResult = _UsersService.RegisterUser(new RequestRegisterUserDto
            {
                Email = request.Email,
                FullName = request.FullName,
                Password = request.Password,
                RePassword = request.RePassword,
                Roles = new List<long>() { 3 }
            });

            if (signeupResult.IsSuccess == true)
            {
                var signinResult = _UsersService.UserLogin(request.Email, request.Password);
                HttpContext.Session.SetString("UserId", signinResult.Data.UserId.ToString());
                HttpContext.Session.SetString("UserName", signinResult.Data.Name);
                HttpContext.Session.SetString("UserEmail", signinResult.Data.Email);
                HttpContext.Session.SetString("UserRole", signinResult.Data.Roles);
                if (signinResult.Data.Image != null) { HttpContext.Session.SetString("UserImage", signinResult.Data.Image.ToString()); }
            }

            return Json(signeupResult);
        }


        //***********Signin***********
        public IActionResult Signin(string ReturnUrl = "/")
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return View("/Viwe/Shared/Error.cshtml");
            }
            ViewBag.url = ReturnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Signin(string Email, string Password)
        {
            var signinResult = _UsersService.UserLogin(Email, Password);
            if (signinResult.IsSuccess == true)
            {
                HttpContext.Session.SetString("UserId", signinResult.Data.UserId.ToString());
                HttpContext.Session.SetString("UserName", signinResult.Data.Name);
                HttpContext.Session.SetString("UserEmail", signinResult.Data.Email);
                HttpContext.Session.SetString("UserRole", signinResult.Data.Roles);
                if (signinResult.Data.Image != null) { HttpContext.Session.SetString("UserImage", signinResult.Data.Image.ToString()); }
            }
            return Json(signinResult);
        }


        //***********SignOut***********
        [CustomAuthorize("کاربر")]
        public IActionResult SignOut(string ReturnUrl = "/")
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("UserEmail");
            HttpContext.Session.Remove("UserRole");
            HttpContext.Session.Remove("UserImage");
            return Redirect("/");
        }


        //***********Edit***********
        [CustomAuthorize("کاربر")]
        public IActionResult Edit(string ReturnUrl = "/")
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                return View("/Viwe/Shared/Error.cshtml");
            }
            ViewBag.url = ReturnUrl;
            return View();
        }

        [HttpPost]
        [CustomAuthorize("کاربر")]
        public IActionResult Edit(RequestEditUserDto request)
        {
            request.Id = Convert.ToInt64(HttpContext.Session.GetString("UserId"));
            var Result = _UsersService.EditMyAccount(request);
            if (Result.IsSuccess == true)
            {
                HttpContext.Session.SetString("UserName", Result.Data.Name);
                HttpContext.Session.SetString("UserEmail", Result.Data.Email);
                if (Result.Data.Image != null) { HttpContext.Session.SetString("UserImage", Result.Data.Image.ToString()); }
            }
            return Json(Result);
        }
    }
}
