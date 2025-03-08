using Microsoft.AspNetCore.Http;
using NewsWebSite.Application.Services.News;
using NewsWebSite.Common.Dto.Users;
using System.Text.RegularExpressions;

namespace NewsWebSite.Application.Services.Authentication
{
    public static class Authentication
    {
        public static GetUserDto UserInfo(this HttpContext context)
        {
            var UserId = context.Session.GetString("UserId");
            var UserName = context.Session.GetString("UserName");
            var UserEmail = context.Session.GetString("UserEmail");

            var result = new GetUserDto
            {
                UserId = Convert.ToInt64(UserId),
                Email = UserEmail,
                FullName = UserName,
            };
            return result;
        }

        
        public static bool IsAdmin(string role)
        {
            try
            {
                string regex = @"^.*مدیر.*$";
                var match = Regex.Match(role, regex, RegexOptions.IgnoreCase);
                return match.Success;
            }
            catch
            {
                return false;
            }
        }


        public static bool IsReporter(string role)
        {
            try
            {
                string regex = @"^.*خبرنگار.*$";
                var match = Regex.Match(role, regex, RegexOptions.IgnoreCase);
                return match.Success;
            }
            catch
            {
                return false;
            }
        }



        public static bool IsOprator(string role)
        {
            try
            {
                string regex = @"^.*کاربر.*$";
                var match = Regex.Match(role, regex, RegexOptions.IgnoreCase);
                return match.Success;
            }
            catch
            {
                return false;
            }
        }
    }
}
