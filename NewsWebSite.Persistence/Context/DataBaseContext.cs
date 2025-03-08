using Microsoft.EntityFrameworkCore;
using NewsWebSite.Application.Interfaces.Context;
using NewsWebSite.Domain.Entities.Users;
using NewsWebSite.Common.Roles;
using NewsWebSite.Domain.Entities.EntitiesNews;

namespace NewsWebSite.Persistence.Context
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInRole> UsersInRoles { get; set; }

        public DbSet<News> News { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<NewsImage> NewsImage { get; set; }
        public DbSet<LikeComment> LikeComment { get; set; }
        public DbSet<LikeNews> LikeNews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = nameof(UserRolesName.Admin), Title = nameof(UserRolesTitle.Admin) });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 2, Name = nameof(UserRolesName.Reporter), Title = nameof(UserRolesTitle.Reporter) });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 3, Name = nameof(UserRolesName.Oprator), Title = nameof(UserRolesTitle.Oprator) });
        }

        public int savechanges()
        {
            return SaveChanges();
        }
    }
}
