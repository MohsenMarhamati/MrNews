using Microsoft.AspNetCore.Http;
using NewsWebSite.Application.Interfaces.Context;
using NewsWebSite.Common.Dto;

namespace NewsWebSite.Application.Services.FileDocument
{
    public class FileDocumentService : IFileDocumentService
    {
        private IDataBaseFile _dbFile;
        public FileDocumentService(IDataBaseFile dbFile)
        {
            _dbFile = dbFile;
        }


        #region AddNewFileDocumentService
        public ResultDto<Guid> AddNewFileDocument(IFormFile File)
        {
            try
            {
                if (File == null)
                {
                    return new ResultDto<Guid>
                    {
                        IsSuccess = false,
                        Message = "درج عکس نا موفق بود"
                    };
                }

                if (File.Length > 1638400)
                {
                    return new ResultDto<Guid>
                    {
                        IsSuccess = false,
                        Message = "حجم عکس نباید از ۱۶۰۰ کیلوبایت بیشتر باشد "
                    };
                }

                var fileDoc = GenerateFile(File);

                if (fileDoc == null)
                {
                    return new ResultDto<Guid>
                    {
                        IsSuccess = false,
                        Message = "درج عکس نا موفق بود"
                    };
                }

                _dbFile.FileDocuments.Add(fileDoc);
                _dbFile.savechanges();

                var result = new ResultDto<Guid>
                {
                    Data = fileDoc.UniqId,
                    IsSuccess = true,
                    Message = "عکس با موفقیت درج شد"
                };

                return result;
            }
            catch (Exception)
            {
                return new ResultDto<Guid>
                {
                    IsSuccess = false,
                    Message = "خطایی پیش آمده است"
                };
            }
        }
        #endregion


        #region GenerateFileService
        private Domain.Entities.FileDocument GenerateFile(IFormFile file)
        {
            try
            {
                var ms = new MemoryStream();
                file.CopyTo(ms);
                var bit = ms.ToArray();

                var fd = new Domain.Entities.FileDocument
                {
                    UniqId = Guid.NewGuid(),
                    Document = bit,
                };

                return fd;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion


        #region EditFileDocumentService
        public ResultDto<Guid> EditFileDocument(Guid id, IFormFile file)
        {
            try
            {
                var OldFile = RemoveFileDocument(id);
                if(OldFile.IsSuccess == false && OldFile.Message != "عکس مورد نظر یافت نشد")
                {
                    return new ResultDto<Guid>
                    {
                        IsSuccess = false,
                        Message = "دسترسی به تصویر ممکن نیست",
                    };
                }

                var NewFile = AddNewFileDocument(file);
                
                return NewFile;
            }
            catch (Exception)
            {
                return new ResultDto<Guid>
                {
                    IsSuccess = false,
                    Message = "عملیات با مشکل رو به رو شد"
                };
            }
        }
        #endregion


        #region RemoveFileDocumentService
        public ResultDto RemoveFileDocument(Guid id)
        {
            try
            {
                var file = _dbFile.FileDocuments.FirstOrDefault(f => f.UniqId == id);
                if (file == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "عکس مورد نظر یافت نشد"
                    };
                }

                _dbFile.FileDocuments.Remove(file);
                _dbFile.savechanges();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "حذف عکس با موفقیت انجام شد"
                };
            }
            catch (Exception)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "عملیات در خواستی نا موفق بود"
                };
            }
        }
        #endregion


        #region GetFileDocumentService
        public ResultDto<byte[]> GetFileDocument(Guid id)
        {
            try
            {
                var file = _dbFile.FileDocuments.Where(f => f.UniqId == id).First();
                if (file == null)
                {
                    return new ResultDto<byte[]>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "عکس مورد نظر یافت نشد"
                    };
                }

                return new ResultDto<byte[]>
                {
                    Data = file.Document,
                    IsSuccess = true,
                    Message = ""
                };
            }
            catch (Exception ex)
            {
                return new ResultDto<byte[]>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "عملیات در خواستی نا موفق بود"
                };
            }
        }
        #endregion
    }
}
