using NewsWebSite.Common.Dto;
using NewsWebSite.Common.Dto.Users;

namespace NewsWebSite.Application.Services.Users
{
    public interface IUsersService
    {
        public ReslutGetUserDto GetUsers(RequestGetUserDto Request);
        public ResultDto<GetUserDto> FindUser(long id);
        public ResultDto<List<RolesDto>> GetRoles();
        public ResultDto AdminEditUser(RequestRegisterUserDto request);
        public ResultDto<ResultUserloginDto> EditMyAccount(RequestEditUserDto request);
        public ResultDto<ResultRegisterUserDto> RegisterUser(RequestRegisterUserDto request);
        public ResultDto RemoveUser(long UserId);
        public ResultDto<ResultUserloginDto> UserLogin(string Username, string Password);
        public ResultDto UserSatusChenge(long UserId);
    }
}
