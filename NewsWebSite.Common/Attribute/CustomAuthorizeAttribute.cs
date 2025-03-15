using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NewsWebSite.Common.Attributes
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        string type;
        public CustomAuthorizeAttribute(string Type)
        {
            type = Type;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var UsrtInfo = context.HttpContext.Session.GetString("UserRole");

            if (UsrtInfo == null)
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                if (!UsrtInfo.Contains(type))
                {
                    context.Result = new UnauthorizedResult();
                }
            }
        }
    }
}
