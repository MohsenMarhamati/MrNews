using Microsoft.EntityFrameworkCore;
using NewsWebSite.Application.Interfaces.Context;
using NewsWebSite.Application.Services.FileDocument;
using NewsWebSite.Common;
using NewsWebSite.Common.Dto;
using NewsWebSite.Common.Dto.Category;
using NewsWebSite.Domain.Entities.EntitiesNews;
using System.Text.RegularExpressions;

namespace NewsWebSite.Application.Services.Categories
{
    public class CategoriesService : ICategoriesService
    {
        public IDataBaseContext _Context;
        public IFileDocumentService _FileService;
        public CategoriesService(IDataBaseContext Context, IFileDocumentService File)
        {
            _Context = Context;
            _FileService = File;
        }

        #region AddNewCategoryService
        public ResultDto AddNewCategory(RequestCategoryDto request)
        {
            try
            {

                if (string.IsNullOrEmpty(request.Name))
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "نام را وارد کنید"
                    };
                }

                string nameRegex = @"^[a-zA-Z]";
                var match1 = Regex.Match(request.Name, nameRegex, RegexOptions.IgnoreCase);
                if (!match1.Success)
                {
                    return new ResultDto()
                    {

                        IsSuccess = false,
                        Message = "نام را لطفا با حروف اینگلیسی وارد کنید ",
                    };
                }

                if (request.Name.Count() < 3)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "نام دسته جدید حداقل باید شامل ۳ کاراکتر باشد"
                    };
                }

                if (_Context.Categories.Any(c => c.Name == request.Name))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "این عنوان لاتین برای گروه خبری دیگر به کار رفته است"
                    };
                }

                if (string.IsNullOrEmpty(request.Title))
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "عنوان را وارد کنید"
                    };
                }

                // "\u0621-\u0628\u062A-\u063A\u0641-\u0642\u0644-\u0648\u064E-\u0651\u0655\u067E\u0686\u0698\u06A9\u06AF\u06BE\u06CC"
                // "^[\x{ 0621}-\x{ 0628}\x{ 062A}-\x{ 063A}\x{ 0641}-\x{ 0642}\x{ 0644}-\x{ 0648}\x{ 064E}-\x{ 0651}\x{ 0655}\x{ 067E}\x{ 0686}\x{ 0698}\x{ 06A9}\x{ 06AF}\x{ 06BE}\x{ 06CC} ]+"
                //string titleRegex = @"^[\u0600-\u06FF\s]+$";
                //var match2 = Regex.Match(request.Title, titleRegex, RegexOptions.IgnoreCase);
                // (!match2.Success)
                //{
                //    return new ResultDto()
                //    {
                //
                //        IsSuccess = false,
                //        Message = "عنوان را لطفا با حروف فارسی وارد کنید ",
                //    };
                //}

                if (request.Title.Count() < 3)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "عنوان دسته جدید حداقل باید شامل ۳ کاراکتر باشد"
                    };
                }

                if (_Context.Categories.Any(p => p.Title == request.Title))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "این عنوان برای گروه خبری دیگر به کار رفته است"
                    };
                }

                Category category = new Category
                {
                    Name = request.Name,
                    Title = request.Title,
                };

                if (request.File != null)
                {
                    var FileDocument = _FileService.AddNewFileDocument(request.File);
                    if (FileDocument.IsSuccess == true && FileDocument.Data != null)
                    {
                        category.FileDocumentId = FileDocument.Data;
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
                else
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "تصویری را به عنوان پوستر انتخاب کنید"
                    };
                }

                _Context.Categories.Add(category);
                _Context.savechanges();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "گروه خبری جدید با موفقیت افزوده شد",
                };
            }
            catch (Exception ex)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "افزودن گروه خبری جدید نا موفق بود",
                };
            }
        }
        #endregion


        #region RemoveCategoryService
        public ResultDto RemoveCategory(long id)
        {
            try
            {
                var category = _Context.Categories.FirstOrDefault(c => c.Id == id);

                if (category == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "گروه خبری یافت نشد"
                    };
                }

                category.RemoveTime = DateTime.Now;
                category.IsRemoved = true;
                _Context.savechanges();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "حذف گروه خبری با موفقیت انجام شد"
                };
            }
            catch (Exception ex)
            {
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "حذف گروه خبری نا موفق بود",
                };
            }
        }
        #endregion


        #region CategorySatusChengeService
        public ResultDto CategorySatusChenge(long id)
        {
            try
            {
                var category = _Context.Categories.FirstOrDefault(c => c.Id == id && c.IsRemoved == false);

                if (category == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "گروه خبری یافت نشد"
                    };
                }

                category.IsActive = !category.IsActive;
                string userstate = category.IsActive == true ? "فعال" : "غیر فعال";
                _Context.savechanges();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = $"!گروه خبری با موفقیت {userstate} شد"
                };
            }
            catch (Exception ex)
            {
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "حذف گروه خبری نا موفق بود",
                };
            }
        }
        #endregion


        #region EditCategoryService
        public ResultDto EditCategory(RequestCategoryDto request)
        {
            try
            {
                var category = _Context.Categories.Where(c => c.Id == request.Id && c.IsRemoved == false).FirstOrDefault();

                string nameRegex = @"^[a-zA-Z]";
                var match1 = Regex.Match(request.Name, nameRegex, RegexOptions.IgnoreCase);
                if (!match1.Success)
                {
                    return new ResultDto()
                    {

                        IsSuccess = false,
                        Message = "نام را لطفا با حروف اینگلیسی وارد کنید ",
                    };
                }


                if (request.Name.Count() < 4 && request.Title.Count() != null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "نام گروه خبری حداقل باید شامل ۳ کاراکتر باشد"
                    };
                }


                if (_Context.Categories.Any(p => p.Name == request.Name && p.Id != request.Id))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "این نام برای گروه خبری دیگر به کار رفته است"
                    };
                }


                //string titleRegex = @"/^[\u0600-\u06FF\s]+$/;";
                //var match2 = Regex.Match(request.Title, titleRegex, RegexOptions.IgnoreCase);
                //if (!match2.Success)
                //{
                //    return new ResultDto()
                //    {

                //        IsSuccess = false,
                //        Message = "عنوان را لطفا با حروف فارسی وارد کنید ",
                //    };
                //}


                if (request.Title.Count() < 4 && request.Title.Count() != null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "این عنوان گروه خبری حداقل باید شامل ۳ کاراکتر باشد"
                    };
                }


                if (_Context.Categories.Any(p => p.Title == request.Title && p.Id != request.Id))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "عنوان برای گروه خبری دیگر به کار رفته است"
                    };
                }


                if (!string.IsNullOrEmpty(request.Name)) { category.Name = request.Name; }
                if (!string.IsNullOrEmpty(request.Title)) { category.Title = request.Title; }
                if (request.File != null)
                {
                    var FileDocument = _FileService.EditFileDocument(category.FileDocumentId, request.File);
                    if (FileDocument.IsSuccess == true && FileDocument.Data != null)
                    {
                        category.FileDocumentId = FileDocument.Data;
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

                category.UpdateTime = DateTime.Now;
                _Context.savechanges();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "ویرایش با موفقیت انجام شد"
                };
            }
            catch (Exception ex)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "ویرایش نا موفقیت بود"
                };
            }
        }
        #endregion


        #region GetCategoriesService
        public ResultDto<ResultCategoryDto> GetCategories(int page, int pagesize)
        {
            try
            {
                var rpweCount = 0;
                var Categories = _Context.Categories;
                var CategoriesDto = Categories
                    .Where(c => c.IsRemoved == false)
                    .ToPaged(page, pagesize, out rpweCount)
                    .Select(c => new CategoryDto
                    {
                        Id = c.Id,
                        Title = c.Title,
                        FileDocumentId = c.FileDocumentId,
                    }).ToList();

                var resultdata = new ResultCategoryDto
                {
                    Categories = CategoriesDto,
                    RecordCount = Categories.Count(),
                    Rowe = rpweCount,
                };

                var result = new ResultDto<ResultCategoryDto>
                {
                    Data = resultdata,
                    IsSuccess = true,
                    Message = ""
                };

                return result;
            }
            catch (Exception)
            {
                return new ResultDto<ResultCategoryDto>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "ارتبات با پایگاه داده با مشکل روبه رو شده است"
                };
            }
        }
        #endregion
        

        #region GetCategoriesForHomeLayoutService
        public ResultDto<List<CategoryDto>> GetCategoriesForHomeLayout()
        {
            try
            {
                var Categories = _Context.Categories;
                var CategoriesDto = Categories.Include(c => c.News)
                    .Where(n => n.IsRemoved == false)
                    .Select(c => new CategoryDto
                    {
                        Name = c.Name,
                        Title = c.Title,
                        Number = c.News.Where(n => n.IsRemoved == false && n.IsActive == true && n.CategoryId == c.Id).Count(),
                        FileDocumentId = c.FileDocumentId,
                    }).ToList();

                var result = new ResultDto<List<CategoryDto>>
                {
                    Data = CategoriesDto,
                    IsSuccess = true,
                    Message = ""
                };

                return result;
            }
            catch (Exception)
            {
                return new ResultDto<List<CategoryDto>>
                {
                    IsSuccess = false,
                    Message = "ارتبات با پایگاه داده با مشکل روبه رو شده است"
                };
            }
        }
        #endregion


        #region GetNewsCategoriesService
        public ResultDto<List<CategoryDto>> GetNewsCategories()
        {
            try
            {
                var Categories = _Context.Categories
                    .Where(c => c.IsRemoved == false)
                    .Select(c => new CategoryDto
                    {
                        Id = c.Id,
                        Title = c.Title,
                    }).ToList();

                var result = new ResultDto<List<CategoryDto>>
                {
                    Data = Categories,
                    IsSuccess = true,
                    Message = ""
                };

                return result;
            }
            catch (Exception)
            {
                return new ResultDto<List<CategoryDto>>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "ارتبات با پایگاه داده با مشکل روبه رو شده است"
                };
            }
        }
        #endregion


        #region FindCategoryService
        public ResultDto<CategoryDto> FindCategory(long id)
        {
            try
            {
                var category = _Context.Categories
                    .Where(c => c.Id == id && c.IsRemoved == false  )
                    .Select(c => new CategoryDto
                {
                    Name = c.Name,
                    Title = c.Title,
                    FileDocumentId = c.FileDocumentId,
                }).SingleOrDefault();

                if (category == null)
                {
                    return new ResultDto<CategoryDto>
                    {
                        IsSuccess = false,
                        Message = "گروه خبری یافت نشد"
                    };
                }

                var result = new ResultDto<CategoryDto>
                {
                    Data = category,
                    IsSuccess = true,
                    Message = ""
                };

                return result;
            }
            catch (Exception)
            {
                return new ResultDto<CategoryDto>
                {
                    IsSuccess = false,
                    Message = "خطایی پیش آمده"
                };
            }
        }
        #endregion
    }

}
