using Microsoft.EntityFrameworkCore;
using NewsWebSite.Application.Interfaces.Context;
using NewsWebSite.Application.Services.FileDocument;
using NewsWebSite.Common;
using NewsWebSite.Common.Dto;
using NewsWebSite.Common.Dto.Users;
using NewsWebSite.Domain.Entities.Users;
using System.Text.RegularExpressions;

namespace NewsWebSite.Application.Services.Users
{
    public class UsersService : IUsersService
    {

        private IDataBaseContext _Context;
        private IFileDocumentService _File;
        public UsersService(IDataBaseContext context, IFileDocumentService file)
        {
            _Context = context;
            _File = file;
        }


        #region GetUsersService
        public ReslutGetUserDto GetUsers(RequestGetUserDto Request)
        {
            int rpweCount = 0;
            var users = _Context.Users;
            var usersList = users
                .Include(u => u.UserInRoles)
                .ThenInclude(r => r.Role)
                .ToPaged(Request.page, Request.pagesize, out rpweCount)
                .Select(p => new GetUserDto
                {
                    Email = p.Email,
                    FullName = p.FullName,
                    Id = p.Id,
                    IsRemoved = p.IsRemoved,
                    IsActive = p.IsActive,
                    FileDocumentId = p.FileDocumentId,
                    UserRoles = p.UserInRoles.Select(r => new RolesDto { Title = r.Role.Title, Id = r.RoleId }).ToList(),
                }).ToList();
            var count = users.Count();

            return new ReslutGetUserDto
            {
                UsersList = usersList,
                RecordCount = count,
                Rowe = rpweCount,
            };
        }
        #endregion


        #region FindUsersService
        public ResultDto<GetUserDto> FindUser(long id)
        {
            try
            {
                var roles = _Context.UsersInRoles.Where(u => u.UserId == id)
                .Select(r => new RolesDto
                {
                    Id = r.RoleId,
                }).ToList();
                var user = _Context.Users.Where(r => r.Id == id).Include(u => u.UserInRoles)
                    .Select(u => new GetUserDto
                    {
                        Id = u.Id,
                        FullName = u.FullName,
                        Email = u.Email,
                        FileDocumentId = u.FileDocumentId,
                        UserRoles = roles
                    })
                    .First();

                if (user == null)
                {
                    return new ResultDto<GetUserDto>
                    {
                        IsSuccess = false,
                        Message = "کاربر یافت نشد",
                    };
                }

                var result = new ResultDto<GetUserDto>
                {
                    IsSuccess = true,
                    Message = "",
                    Data = user
                };

                return result;
            }
            catch (Exception)
            {
                return new ResultDto<GetUserDto>
                {
                    IsSuccess = false,
                    Message = "خطایی رخ داده است"
                };
            }
        }
        #endregion


        #region GetRolesService
        public ResultDto<List<RolesDto>> GetRoles()
        {
            var roles = _Context.Roles.Select(p => new RolesDto
            {
                Id = p.Id,
                Name = p.Name,
                Title = p.Title,
            }).ToList();

            return new ResultDto<List<RolesDto>>()
            {
                Data = roles,
                IsSuccess = true,
                Message = "",
            };
        }
        #endregion


        #region AdminEditUserService
        public ResultDto AdminEditUser(RequestRegisterUserDto request)
        {
            try
            {
                var user = _Context.Users.Where(a => a.Id == request.Id).Include(u => u.UserInRoles).First();

                if (user == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "کاربر یافت نشد"
                    };
                }

                if (!string.IsNullOrEmpty(request.FullName))
                {
                    user.FullName = request.FullName;
                }

                if (_Context.Users.Any(p => p.Email == request.Email && p.Id != request.Id))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "پست الکترونیک تکراری است"
                    };
                }

                string emailRegex = @"^[a-zA-Z]+[a-zA-Z0-9\.]*@[\w\b\-_]+\.[A-Z]{2,}$";
                var match = Regex.Match(request.Email, emailRegex, RegexOptions.IgnoreCase);
                if (!match.Success)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "ایمیل را صحیح وارد نمایید",
                    };
                }

                if (!string.IsNullOrEmpty(request.Email))
                {
                    user.Email = request.Email;
                }

                if (request.Roles.Count == 0 || request.Roles.Any(r => r == 0))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "نقش را وارد نمایید"
                    };
                }

                List<UserInRole> usersInRoles = new List<UserInRole>();
                foreach (var item in request.Roles)
                {
                    usersInRoles.Add(new UserInRole()
                    {
                        RoleId = item,
                        UserId = user.Id,
                    });
                }

                if (request.File != null)
                {
                    var FileDocument = _File.AddNewFileDocument(request.File);
                    if (FileDocument.IsSuccess == true && FileDocument.Data != null)
                    {
                        user.FileDocumentId = FileDocument.Data;
                    }
                    else
                    {
                        return new ResultDto
                        {
                            IsSuccess = false,
                            Message = FileDocument.Message
                        };
                    }
                }

                user.UserInRoles = usersInRoles;
                user.UpdateTime = DateTime.Now;
                _Context.savechanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "ویرایش با موفقیت انجام شد"
                };
            }
            catch (Exception)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "ثبت نام انجام نشد"
                };
            }
        }
        #endregion


        #region EditMyAccountService
        public ResultDto<ResultUserloginDto> EditMyAccount(RequestEditUserDto request)
        {
            try
            {
                var user = _Context.Users.Where(a => a.Id == request.Id).Include(u => u.UserInRoles).First();

                if (user == null)
                {
                    return new ResultDto<ResultUserloginDto>
                    {
                        IsSuccess = false,
                        Message = "کاربر یافت نشد"
                    };
                }

                if (_Context.Users.Any(p => p.Email == request.EditEmail && p.Id != request.Id))
                {
                    return new ResultDto<ResultUserloginDto>
                    {
                        IsSuccess = false,
                        Message = "پست الکترونیک تکراری است"
                    };
                }

                string emailRegex = @"^[a-zA-Z]+[a-zA-Z0-9\.]*@[\w\b\-_]+\.[A-Z]{2,}$";
                var match = Regex.Match(request.EditEmail, emailRegex, RegexOptions.IgnoreCase);
                if (!match.Success)
                {
                    return new ResultDto<ResultUserloginDto>
                    {
                        IsSuccess = false,
                        Message = "ایمیل را صحیح وارد نمایید",
                    };
                }

                if (string.IsNullOrWhiteSpace(request.Password))
                {
                    return new ResultDto<ResultUserloginDto>
                    {
                        IsSuccess = false,
                        Message = "رمز عبور را وارد نمایید",
                    };
                }

                if (!Hash.VerifyPassword(user.Password, user.PasswordSalt, request.Password))
                {
                    return new ResultDto<ResultUserloginDto>
                    {
                        IsSuccess = false,
                        Message = "رمز وارد شده اشتباه است!",
                    };
                }

                if (request.EditPassword != request.EditRePassword)
                {
                    return new ResultDto<ResultUserloginDto>
                    {
                        IsSuccess = false,
                        Message = "رمز عبور جدید و تکرار آن برابر نیست",
                    };
                }

                if (request.Password.Length < 8)
                {
                    return new ResultDto<ResultUserloginDto>
                    {
                        IsSuccess = false,
                        Message = "رمز عبور باید حداقل 8 کاراکتر باشد",
                    };
                }

                if (request.EditFullName != null && request.EditFullName != user.FullName) { user.FullName = request.EditFullName; }
                if (request.EditEmail != null && request.EditEmail != user.Email) { user.Email = request.EditEmail; }
                if (request.EditPassword != null) { user.Password = Hash.PasswordHash(request.Password, user.PasswordSalt); }

                if (request.EditFile != null)
                {
                    var FileDocument = user.FileDocumentId == null ? _File.AddNewFileDocument(request.EditFile) : _File.EditFileDocument(user.FileDocumentId.Value, request.EditFile);
                    if (FileDocument.IsSuccess == true && FileDocument.Data != null)
                    {
                        user.FileDocumentId = FileDocument.Data;
                    }
                    else
                    {
                        return new ResultDto<ResultUserloginDto>
                        {
                            IsSuccess = false,
                            Message = FileDocument.Message
                        };
                    }
                }

                user.UpdateTime = DateTime.Now;
                _Context.savechanges();

                var data = new ResultUserloginDto
                {
                    Email = user.Email,
                    Image = user.FileDocumentId,
                    Name = user.FullName
                };

                var result = new ResultDto<ResultUserloginDto>
                {
                    Data = data,
                    IsSuccess = true,
                    Message = "ویرایش با موفقیت انجام شد"
                };

                return result;
            }
            catch (Exception)
            {
                return new ResultDto<ResultUserloginDto>
                {
                    IsSuccess = false,
                    Message = "ثبت نام انجام نشد"
                };
            }
        }
        #endregion


        #region RegisterUserService
        public ResultDto<ResultRegisterUserDto> RegisterUser(RequestRegisterUserDto request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.FullName))
                {
                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto()
                        {
                            UserId = 0
                        },
                        IsSuccess = false,
                        Message = "نام را وارد نمایید"
                    };
                }

                if (string.IsNullOrEmpty(request.Email))
                {
                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto()
                        {
                            UserId = 0
                        },
                        IsSuccess = false,
                        Message = "ایمیل خود را وارد نمایید"
                    };
                }

                string emailRegex = @"^[a-zA-Z]+[a-zA-Z0-9]*@[\w\b\-_]+\.[A-Z]{2,}$";
                var match = Regex.Match(request.Email, emailRegex, RegexOptions.IgnoreCase);
                if (!match.Success)
                {
                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto()
                        {
                            UserId = 0
                        },
                        IsSuccess = false,
                        Message = "ایمیل خود را به درستی وارد نمایید",
                    };
                }

                if (_Context.Users.Any(p => p.Email == request.Email))
                {
                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto()
                        {
                            UserId = 0
                        },
                        IsSuccess = false,
                        Message = "ایمیل تکراری می باشد",
                    };
                }

                if (request.Roles.Count == 0 || request.Roles.Where(r => r == 0).Count() == 1)
                {

                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto()
                        {
                            UserId = 0
                        },
                        IsSuccess = false,
                        Message = "نقش را وارد نمایید",
                    };
                }

                if (string.IsNullOrEmpty(request.Password) && string.IsNullOrEmpty(request.RePassword))
                {

                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto()
                        {
                            UserId = 0
                        },
                        IsSuccess = false,
                        Message = "رمز عبور را وارد نمایید",
                    };
                }

                if (request.Password.Length < 8)
                {
                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto()
                        {
                            UserId = 0
                        },
                        IsSuccess = false,
                        Message = "رمز عبور باید حداقل 8 کاراکتر باشد",
                    };
                }

                if (request.Password != request.RePassword)
                {
                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto()
                        {
                            UserId = 0
                        },
                        IsSuccess = false,
                        Message = "رمز عبور و تکرار آن برابر نیست",
                    };
                }

                var PasswordSalt = Guid.NewGuid().ToString();
                User user = new User()
                {
                    Email = request.Email,
                    FullName = request.FullName,
                    IsActive = true,
                    PasswordSalt = PasswordSalt,
                    Password = Hash.PasswordHash(request.Password, PasswordSalt),
                };

                if (request.File != null)
                {
                    var FileDocument = _File.AddNewFileDocument(request.File);
                    if (FileDocument.IsSuccess == true && FileDocument.Data != null)
                    {
                        user.FileDocumentId = FileDocument.Data;
                    }
                }

                List<UserInRole> usersInRoles = new List<UserInRole>();
                foreach (var item in request.Roles)
                {
                    var roles = _Context.Roles.Find(item);
                    if (roles != null)
                    {
                        usersInRoles.Add(new UserInRole()
                        {
                            Role = roles,
                            RoleId = roles.Id,
                            User = user,
                            UserId = item,
                        });
                    }
                }

                user.UserInRoles = usersInRoles;
                _Context.Users.Add(user);
                _Context.savechanges();

                return new ResultDto<ResultRegisterUserDto>()
                {
                    Data = new ResultRegisterUserDto()
                    {
                        UserId = user.Id,
                    },
                    IsSuccess = true,
                    Message = "ثبت نام انجام شد"
                };
            }
            catch (Exception)
            {
                return new ResultDto<ResultRegisterUserDto>()
                {
                    Data = new ResultRegisterUserDto()
                    {
                        UserId = 0
                    },
                    IsSuccess = false,
                    Message = "ثبت نام انجام نشد"
                };
            }
        }
        #endregion


        #region RemoveUserService
        public ResultDto RemoveUser(long UserId)
        {
            try
            {
                var user = _Context.Users.Find(UserId);
                if (user == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "کاربر یافت نشد",
                    };
                }
                user.RemoveTime = DateTime.Now;
                user.IsRemoved = true;
                _Context.savechanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "کاربر با موفقیت حذف شد",
                };
            }
            catch (Exception)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "حذف کاربر ناموفق بود",
                };
            }
        }
        #endregion


        #region UserLoginService
        public ResultDto<ResultUserloginDto> UserLogin(string Email, string Password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Email))
                {
                    return new ResultDto<ResultUserloginDto>()
                    {
                        Data = new ResultUserloginDto() { },
                        IsSuccess = false,
                        Message = "ایمیل را وارد نمایید",
                    };
                }

                string emailRegex = @"^[a-zA-Z]+[a-zA-Z0-9]*@[\w\b\-_]+\.[A-Z]{2,}$";
                var match = Regex.Match(Email, emailRegex, RegexOptions.IgnoreCase);
                if (!match.Success)
                {
                    return new ResultDto<ResultUserloginDto>()
                    {
                        Data = new ResultUserloginDto() { },
                        IsSuccess = false,
                        Message = "ایمیل را صحیح وارد نمایید",
                    };
                }

                if (string.IsNullOrWhiteSpace(Password))
                {
                    return new ResultDto<ResultUserloginDto>()
                    {
                        Data = new ResultUserloginDto() { },
                        IsSuccess = false,
                        Message = "رمز عبور را وارد نمایید",
                    };
                }

                var user = _Context.Users.Include(p => p.UserInRoles).ThenInclude(p => p.Role)
                .Where(p => p.Email.Equals(Email) && p.IsRemoved == false).FirstOrDefault();

                if (user == null)
                {
                    return new ResultDto<ResultUserloginDto>()
                    {
                        Data = new ResultUserloginDto() { },
                        IsSuccess = false,
                        Message = "کاربری با این ایمیل در این سایت ثبت نام نکرده است",
                    };
                }

                if (!user.IsActive)
                {
                    return new ResultDto<ResultUserloginDto>()
                    {
                        Data = new ResultUserloginDto() { },
                        IsSuccess = false,
                        Message = "حساب کاربری شما غیر فعال است",
                    };
                }

                if (!Hash.VerifyPassword(user.Password, user.PasswordSalt, Password))
                {
                    return new ResultDto<ResultUserloginDto>()
                    {
                        Data = new ResultUserloginDto() { },
                        IsSuccess = false,
                        Message = "رمز وارد شده اشتباه است!",
                    };
                }

                var roles = "";
                foreach (var item in user.UserInRoles)
                {
                    roles += $"{item.Role.Title}";
                }
              

                var result = new ResultDto<ResultUserloginDto>()
                {
                    Data = new ResultUserloginDto()
                    {
                        Roles = roles,
                        UserId = user.Id,
                        Name = user.FullName,
                        Email = user.Email,
                        Image = user.FileDocumentId
                    },
                    IsSuccess = true,
                    Message = "ورود به سایت با موفقیت انجام شد",
                };

                return result;
            }
            catch (Exception)
            {
                return new ResultDto<ResultUserloginDto>()
                {
                    Data = new ResultUserloginDto() { },
                    IsSuccess = false,
                    Message = "ورود به سایت نا موفق بود",
                };
            }
        }
        #endregion


        #region UserSatusChengeService
        public ResultDto UserSatusChenge(long UserId)
        {
            try
            {
                var user = _Context.Users.Find(UserId);
                if (user == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "کاربر یافت نشد"
                    };
                }
                user.IsActive = !user.IsActive;
                _Context.savechanges();
                string userstate = user.IsActive == true ? "فعال" : "غیر فعال";
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = $"!کاربر با موفقیت {userstate} شد"
                };
            }
            catch (Exception)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = $"مشکلی پیش آمده است"
                };
            }
        }
        #endregion


    }
}
