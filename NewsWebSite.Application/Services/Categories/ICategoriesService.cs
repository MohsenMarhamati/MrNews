using NewsWebSite.Common.Dto;
using NewsWebSite.Common.Dto.Category;

namespace NewsWebSite.Application.Services.Categories
{
    public interface ICategoriesService
    {
        public ResultDto AddNewCategory(RequestCategoryDto request);
        public ResultDto RemoveCategory(long id);
        public ResultDto CategorySatusChenge(long id);
        public ResultDto EditCategory(RequestCategoryDto request);
        public ResultDto<ResultCategoryDto> GetCategories(int page, int pagesize = 5);
        public ResultDto<List<CategoryDto>> GetCategoriesForHomeLayout();
        public ResultDto<List<CategoryDto>> GetNewsCategories();
        public ResultDto<CategoryDto> FindCategory(long id);
    }

}
