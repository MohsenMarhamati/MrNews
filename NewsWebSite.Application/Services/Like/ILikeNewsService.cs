using NewsWebSite.Common.Dto;
using NewsWebSite.Common.Dto.Like;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsWebSite.Application.Services.Like
{
    public interface ILikeNewsService
    {
        public ResultDto<LikeNewsDto> AddLike(LikeNewsDto request);
    }
}
