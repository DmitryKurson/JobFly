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
                .OrderBy(c => c.Title) // Наприклад, сортуючи по назві
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategories(string? title, VacancySortState sortOrder, int page, int pageSize)
        {
            IQueryable<Category> categories = _db.Categories;

            if (!string.IsNullOrEmpty(title))
            {
                categories = categories.Where(p => p.Title!.Contains(title));
            }

            categories = sortOrder switch
            {
                VacancySortState.IdAsc => categories.OrderBy(s => s.Id),
                VacancySortState.IdDesc => categories.OrderByDescending(s => s.Id),
                VacancySortState.TitleAsc => categories.OrderBy(s => s.Title),
                VacancySortState.TitleDesc => categories.OrderByDescending(s => s.Title),
                //VacancySortState.SalaryAsc => categories.OrderBy(s => s.Salary),
                //VacancySortState.SalaryDesc => categories.OrderByDescending(s => s.Salary),
                //VacancySortState.StatusAsc => categories.OrderBy(s => s.Status),
                //VacancySortState.StatusDesc => categories.OrderByDescending(s => s.Status),
                _ => categories.OrderBy(s => s.Id),
            };

            return await categories.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetCategoriesCount(string? title)
        {
            IQueryable<Vacancy> projects = _db.Vacancies;

            if (!string.IsNullOrEmpty(title))
            {
                projects = projects.Where(p => p.Title!.Contains(title));
            }

            return await projects.CountAsync();
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

