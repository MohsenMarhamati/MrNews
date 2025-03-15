using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsWebSite.Common.Dto.News
{
    public class ResultGetNewsForTable
    {
        public List<GetNewsView> Data { get; set; } = new List<GetNewsView>();
        public int RecordCount { get; set; }
        public int Rowe { get; set; }
    }
}
