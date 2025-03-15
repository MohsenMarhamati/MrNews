using Microsoft.AspNetCore.Mvc;
using NewsWebSite.Application.Services.FileDocument;
using NewsWebSite.Common.Dto;
using NewsWebSite.Common.Dto.File;

namespace EndPoint.Controllers
{
    public class FileDacumentsController : Controller
    {
        public IFileDocumentService _FileService;
        public FileDacumentsController(IFileDocumentService file)
        {
            _FileService = file;
        }

        public IActionResult GenerateFile(FileDto file)
        {
            try
            {
                var result = _FileService.GetFileDocument(file.FileDocumentId.Value).Data;
                if (result == null ) return Ok("~/ SiteTemplate / assets / images / man.png");
                else return new FileContentResult(result, "image/jpeg");
            }
            catch
            {
                return Ok( new
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمده است"
                });
            }

        }

        [HttpPost]
        public IActionResult CreateFile(IFormFile File)
        {
            var result = _FileService.AddNewFileDocument(File);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult RemoveFile(Guid id)
        {
            var result = _FileService.RemoveFileDocument(id);
            return Ok(result);
        }
    }
}
