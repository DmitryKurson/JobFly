using JobFly.Models;
using JobFly.ViewModels;

namespace JobFly.Areas.Employer.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories(string? title, CategorySortState sortOrder, int page, int pageSize);
        Task<IEnumerable<Category>> GetAll();
        Task<int> GetCategoriesCount(string? title);
        Task<Category?> GetCategoryById(int? id);   
        Task Create(Category category);
        Task Update(Category category);
        Task Delete(int? id);
    }
}
