using JobFly.Data;
using JobFly.Models;
using JobFly.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JobFly.Areas.Employer.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _db;

        public CategoryService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _db.Categories
                .OrderBy(c => c.Title) 
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategories(string? title, CategorySortState sortOrder, int page, int pageSize)
        {
            IQueryable<Category> categories = _db.Categories;

            if (!string.IsNullOrEmpty(title))
            {
                categories = categories.Where(p => p.Title!.Contains(title));
            }

            categories = sortOrder switch
            {
                CategorySortState.IdAsc => categories.OrderBy(s => s.Id),
                CategorySortState.IdDesc => categories.OrderByDescending(s => s.Id),
                CategorySortState.TitleAsc => categories.OrderBy(s => s.Title),
                CategorySortState.TitleDesc => categories.OrderByDescending(s => s.Title),               
                _ => categories.OrderBy(s => s.Id),
            };

            return await categories.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetCategoriesCount(string? title)
        {
            IQueryable<Category> categories = _db.Categories;

            if (!string.IsNullOrEmpty(title))
            {
                categories = categories.Where(p => p.Title!.Contains(title));
            }

            return await categories.CountAsync();
        }

        public async Task<Category?> GetCategoryById(int? id)
        {
            return await _db.Categories.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Create(Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Category category)
        {
            _db.Categories.Update(category);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            var category = await GetCategoryById((int)id);
            if (category != null)
            {
                _db.Categories.Remove(category);
                await _db.SaveChangesAsync();
            }
        }
    }
}

