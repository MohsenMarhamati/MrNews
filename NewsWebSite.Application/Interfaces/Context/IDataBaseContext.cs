using Microsoft.EntityFrameworkCore;
using NewsWebSite.Domain.Entities.EntitiesNews;
using NewsWebSite.Domain.Entities.Users;

namespace NewsWebSite.Application.Interfaces.Context
{
    public interface IDataBaseContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInRole> UsersInRoles { get; set; }

        public DbSet<News> News { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<NewsImage> NewsImage { get; set; }
        public DbSet<LikeComment> LikeComment { get; set; }
        public DbSet<LikeNews> LikeNews { get; set; }

        public int savechanges();
    } 
}
