using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsWebSite.Common.Dto.Comment
{
    public class GetCommentsDto
    {
        public List<CommentDto> List { get; set; }
        public int RecordCount { get; set; }
        public int Rowe { get; set; }
    }
}
