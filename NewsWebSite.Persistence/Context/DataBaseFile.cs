using Microsoft.EntityFrameworkCore;
using NewsWebSite.Application.Interfaces.Context;
using NewsWebSite.Domain.Entities;

namespace NewsWebSite.Persistence.Context
{
    public class DataBaseFile : DbContext,IDataBaseFile
    {
        public DataBaseFile(DbContextOptions<DataBaseFile> options) : base(options)
        {

        }

        public DbSet<FileDocument> FileDocuments { get; set; }
       
        public int savechanges()
        {
            return SaveChanges();
        }
    }
}
