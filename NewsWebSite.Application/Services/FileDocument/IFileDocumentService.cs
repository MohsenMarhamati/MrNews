using Microsoft.AspNetCore.Http;
using NewsWebSite.Common.Dto;

namespace NewsWebSite.Application.Services.FileDocument
{
    public interface IFileDocumentService
    {
        public ResultDto<Guid> AddNewFileDocument(IFormFile File);
        public ResultDto<Guid> EditFileDocument(Guid id, IFormFile file);
        public ResultDto RemoveFileDocument(Guid id);
        public ResultDto<byte[]> GetFileDocument(Guid id);
    }
}
