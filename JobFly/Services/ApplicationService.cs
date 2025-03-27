using JobFly.Data;
using JobFly.Models;
using JobFly.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JobFly.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly ApplicationDbContext _db;

        public ApplicationService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Application?> GetApplicationById(int? id)
        {
            return await _db.Applications
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Create(Application application)
        {
            _db.Applications.Add(application);
            await _db.SaveChangesAsync();
        }



        public async Task Delete(int? id)
        {
            var application = await GetApplicationById((int)id);
            if (application != null)
            {
                _db.Applications.Remove(application);
                await _db.SaveChangesAsync();
            }
        }


        //public async Task Update(Vacancy vacancy)
        //{
        //    _db.Vacancies.Update(vacancy);
        //    await _db.SaveChangesAsync();
        //}


        //public async Task<IEnumerable<Vacancy>> GetApplications(string? title, int? categoryId, VacancySortState sortOrder, int page, int pageSize)
        //{
        //    IQueryable<Application> applications = _db.Applications
        //        .Include(v => v.Category)
        //        .Include(v => v.Employer);

        //    if (!string.IsNullOrEmpty(title))
        //    {
        //        applications = applications.Where(p => p.Title.Contains(title));
        //    }

        //    if (categoryId.HasValue)
        //    {
        //        applications = applications.Where(v => v.CategoryId == categoryId);
        //    }

        //    applications = sortOrder switch
        //    {
        //        VacancySortState.IdAsc => applications.OrderBy(s => s.Id),
        //        VacancySortState.IdDesc => applications.OrderByDescending(s => s.Id),
        //        VacancySortState.TitleAsc => applications.OrderBy(s => s.Title),
        //        VacancySortState.TitleDesc => applications.OrderByDescending(s => s.Title),
        //        VacancySortState.SalaryAsc => applications.OrderBy(s => s.Salary),
        //        VacancySortState.SalaryDesc => applications.OrderByDescending(s => s.Salary),
        //        VacancySortState.StatusAsc => applications.OrderBy(s => s.IsActive),
        //        VacancySortState.StatusDesc => applications.OrderByDescending(s => s.IsActive),
        //        VacancySortState.CategoryAsc => applications.OrderBy(v => v.Category.Title),
        //        VacancySortState.CategoryDesc => applications.OrderByDescending(v => v.Category.Title),
        //        VacancySortState.EmployerAsc => applications.OrderBy(v => v.Employer.CompanyTitle),
        //        VacancySortState.EmployerDesc => applications.OrderByDescending(v => v.Employer.CompanyTitle),
        //        _ => applications.OrderBy(s => s.Id),
        //    };

        //    return await applications.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        //}

        //public async Task<int> GetVacanciesCount(string? title, int? categoryId)
        //{
        //    IQueryable<Vacancy> query = _db.Vacancies;

        //    if (!string.IsNullOrEmpty(title))
        //    {
        //        query = query.Where(p => p.Title.Contains(title));
        //    }

        //    if (categoryId.HasValue)
        //    {
        //        query = query.Where(v => v.CategoryId == categoryId);
        //    }

        //    return await query.CountAsync();
        //}



        //public async Task<List<Vacancy>> GetVacanciesForEmployer(string employerId, string title, int? categoryId, VacancySortState sortOrder, int page, int pageSize)
        //{
        //    var query = _db.Vacancies
        //        .Include(v => v.Category)
        //        .Where(v => v.EmployerId == employerId);

        //    if (!string.IsNullOrEmpty(title))
        //    {
        //        query = query.Where(v => v.Title.Contains(title));
        //    }

        //    if (categoryId.HasValue)
        //    {
        //        query = query.Where(v => v.CategoryId == categoryId);
        //    }

        //    query = sortOrder switch
        //    {
        //        VacancySortState.IdAsc => query.OrderBy(s => s.Id),
        //        VacancySortState.IdDesc => query.OrderByDescending(s => s.Id),
        //        VacancySortState.TitleAsc => query.OrderBy(s => s.Title),
        //        VacancySortState.TitleDesc => query.OrderByDescending(s => s.Title),
        //        VacancySortState.SalaryAsc => query.OrderBy(s => s.Salary),
        //        VacancySortState.SalaryDesc => query.OrderByDescending(s => s.Salary),
        //        VacancySortState.CategoryAsc => query.OrderBy(v => v.Category.Title),
        //        VacancySortState.CategoryDesc => query.OrderByDescending(v => v.Category.Title),
        //        _ => query.OrderBy(v => v.Id),
        //    };

        //    query = query.Skip((page - 1) * pageSize).Take(pageSize);

        //    return await query.ToListAsync();
        //}

        //public async Task<int> GetVacanciesCountForEmployer(string employerId, string? title, int? categoryId)
        //{
        //    var query = _db.Vacancies.Where(v => v.EmployerId == employerId);

        //    if (!string.IsNullOrEmpty(title))
        //    {
        //        query = query.Where(v => v.Title.Contains(title));
        //    }

        //    if (categoryId.HasValue)
        //    {
        //        query = query.Where(v => v.CategoryId == categoryId);
        //    }

        //    return await query.CountAsync();
        //}
    }
}
