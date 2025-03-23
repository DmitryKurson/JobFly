using JobFly.Data;
using JobFly.Models;
using JobFly.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JobFly.Areas.Employer.Services
{
    public class VacancyService : IVacancyService
    {
        private readonly ApplicationDbContext _db;

        public VacancyService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Vacancy>> GetVacancies(string? title, VacancySortState sortOrder, int page, int pageSize)
        {
            IQueryable<Vacancy> vacancies = _db.Vacancies;

            if (!string.IsNullOrEmpty(title))
            {
                vacancies = vacancies.Where(p => p.Title!.Contains(title));
            }

            vacancies = sortOrder switch
            {
                VacancySortState.IdAsc => vacancies.OrderBy(s => s.Id),
                VacancySortState.IdDesc => vacancies.OrderByDescending(s => s.Id),
                VacancySortState.TitleAsc => vacancies.OrderBy(s => s.Title),
                VacancySortState.TitleDesc => vacancies.OrderByDescending(s => s.Title),
                VacancySortState.SalaryAsc => vacancies.OrderBy(s => s.Salary),
                VacancySortState.SalaryDesc => vacancies.OrderByDescending(s => s.Salary),
                VacancySortState.StatusAsc => vacancies.OrderBy(s => s.IsActive),
                VacancySortState.StatusDesc => vacancies.OrderByDescending(s => s.IsActive),
                _ => vacancies.OrderBy(s => s.Id),
            };

            return await vacancies.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetVacanciesCount(string? title)
        {
            IQueryable<Vacancy> projects = _db.Vacancies;

            if (!string.IsNullOrEmpty(title))
            {
                projects = projects.Where(p => p.Title!.Contains(title));
            }

            return await projects.CountAsync();
        }

        public async Task<Vacancy?> GetVacancyById(int? id)
        {
            return await _db.Vacancies.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Create(Vacancy project)
        {
            _db.Vacancies.Add(project);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Vacancy project)
        {
            _db.Vacancies.Update(project);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            var project = await GetVacancyById((int)id);
            if (project != null)
            {
                _db.Vacancies.Remove(project);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Vacancy>> GetVacanciesForEmployer(string employerId, string? title, VacancySortState sortOrder, int page, int pageSize)
        {
            var query = _db.Vacancies.Where(v => v.EmployerId == employerId);

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(v => v.Title.Contains(title));
            }

            query = sortOrder switch
            {
                VacancySortState.IdAsc => query.OrderBy(s => s.Id),
                VacancySortState.IdDesc => query.OrderByDescending(s => s.Id),
                VacancySortState.TitleAsc => query.OrderBy(s => s.Title),
                VacancySortState.TitleDesc => query.OrderByDescending(s => s.Title),
                VacancySortState.SalaryAsc => query.OrderBy(s => s.Salary),
                VacancySortState.SalaryDesc => query.OrderByDescending(s => s.Salary),
                VacancySortState.StatusAsc => query.OrderBy(s => s.IsActive),
                VacancySortState.StatusDesc => query.OrderByDescending(s => s.IsActive),
                _ => query.OrderBy(s => s.Id),
            };

            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetVacanciesCountForEmployer(string employerId, string? title)
        {
            var query = _db.Vacancies.Where(v => v.EmployerId == employerId);

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(v => v.Title.Contains(title));
            }

            return await query.CountAsync();
        }
    }
}

