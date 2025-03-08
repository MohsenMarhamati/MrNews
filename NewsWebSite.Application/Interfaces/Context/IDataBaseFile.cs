using Microsoft.EntityFrameworkCore;
using NewsWebSite.Domain.Entities;

namespace NewsWebSite.Application.Interfaces.Context
{
    public interface IDataBaseFile
    {
        public DbSet<FileDocument> FileDocuments { get; set; }

        public int savechanges();
    }
}
