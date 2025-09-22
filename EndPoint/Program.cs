using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using NewsWebSite.Application.Interfaces.Context;
using NewsWebSite.Persistence.Context;
using NewsWebSite.Application.Services.Users;
using NewsWebSite.Application.Services.Categories;
using NewsWebSite.Application.Services.FileDocument;
using NewsWebSite.Application.Services.News;
using NewsWebSite.Application.Services.Like;
using NewsWebSite.Application.Services.Comment;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();
builder.Services.AddScoped<IDataBaseFile, DataBaseFile>();
builder.Services.AddScoped<IFileDocumentService, FileDocumentService>();
builder.Services.AddScoped<ILikeNewsService, LikeNewsService>();
builder.Services.AddScoped<ILikeCommentService, LikeCommentService>();

// Add services to the Userscontainer.
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddSession();
// Add services to the CategoriesContainer.
builder.Services.AddScoped<ICategoriesService, CategoriesService>();

// Add services to the NewsContainer.
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<ICommentsService, CommentsService>();


var ConnectionString = builder.Configuration.GetConnectionString("Name");
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<DataBaseContext>(option => option.UseSqlServer(ConnectionString));

var ConnectionStringFile = builder.Configuration.GetConnectionString("File");
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<DataBaseFile>(option => option.UseSqlServer(ConnectionStringFile));

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(options =>
{
    options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = new PathString("/");
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
   name: "areas",
   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.Run();
