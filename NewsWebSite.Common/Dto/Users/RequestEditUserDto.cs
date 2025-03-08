using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsWebSite.Common.Dto.Users
{
    public class RequestEditUserDto
    {
        public long? Id { get; set; }
        public string? EditFullName { get; set; }
        public string? EditEmail { get; set; }
        public string? Password { get; set; }
        public string? EditPassword { get; set; }
        public string? EditRePassword { get; set; }
        public IFormFile? EditFile { get; set; }
    }
}
