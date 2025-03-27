using JobFly.Data;
using JobFly.Models;
using JobFly.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFly.Areas.Employer.Services
{
    public class VacancyService : IVacancyService
    {
        private readonly ApplicationDbContext _db;

        public VacancyService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Vacancy>> GetVacancies(string? title, int? categoryId, VacancySortState sortOrder, int page, int pageSize)
        {
            IQueryable<Vacancy> vacancies = _db.Vacancies
                .Include(v => v.Category)
                .Include(v => v.Employer);

            if (!string.IsNullOrEmpty(title))
            {
                vacancies = vacancies.Where(p => p.Title.Contains(title));
            }

            if (categoryId.HasValue)
            {
                vacancies = vacancies.Where(v => v.CategoryId == categoryId);
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
                VacancySortState.CategoryAsc => vacancies.OrderBy(v => v.Category.Title),
                VacancySortState.CategoryDesc => vacancies.OrderByDescending(v => v.Category.Title),
                VacancySortState.EmployerAsc => vacancies.OrderBy(v => v.Employer.CompanyTitle),
                VacancySortState.EmployerDesc => vacancies.OrderByDescending(v => v.Employer.CompanyTitle),
                _ => vacancies.OrderBy(s => s.Id),
            };

            return await vacancies.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetVacanciesCount(string? title, int? categoryId)
        {
            IQueryable<Vacancy> query = _db.Vacancies;

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(p => p.Title.Contains(title));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(v => v.CategoryId == categoryId);
            }

            return await query.CountAsync();
        }

        public async Task<Vacancy?> GetVacancyById(int id)
        {
            return await _db.Vacancies
                .Include(v => v.Category)
                .Include(v => v.Employer)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Create(Vacancy vacancy)
        {
            _db.Vacancies.Add(vacancy);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Vacancy vacancy)
        {
            _db.Vacancies.Update(vacancy);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            var vacancy = await GetVacancyById((int)id);
            if (vacancy != null)
            {
                _db.Vacancies.Remove(vacancy);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<Vacancy>> GetVacanciesForEmployer(string employerId, string title, int? categoryId, VacancySortState sortOrder, int page, int pageSize)
        {
            var query = _db.Vacancies
                .Include(v => v.Category)
                .Where(v => v.EmployerId == employerId);

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(v => v.Title.Contains(title));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(v => v.CategoryId == categoryId);
            }

            query = sortOrder switch
            {
                VacancySortState.IdAsc => query.OrderBy(s => s.Id),
                VacancySortState.IdDesc => query.OrderByDescending(s => s.Id),
                VacancySortState.TitleAsc => query.OrderBy(s => s.Title),
                VacancySortState.TitleDesc => query.OrderByDescending(s => s.Title),
                VacancySortState.SalaryAsc => query.OrderBy(s => s.Salary),
                VacancySortState.SalaryDesc => query.OrderByDescending(s => s.Salary),
                VacancySortState.CategoryAsc => query.OrderBy(v => v.Category.Title),
                VacancySortState.CategoryDesc => query.OrderByDescending(v => v.Category.Title),
                _ => query.OrderBy(v => v.Id),
            };

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<int> GetVacanciesCountForEmployer(string employerId, string? title, int? categoryId)
        {
            var query = _db.Vacancies.Where(v => v.EmployerId == employerId);

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(v => v.Title.Contains(title));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(v => v.CategoryId == categoryId);
            }

            return await query.CountAsync();
        }
        public async Task<List<Application>> GetApplicationsForVacancy(int vacancyId)
        {
            var applications = await _db.Applications
               .Where(a => a.VacancyId == vacancyId)
               .Include(a => a.Employee)
                   .ThenInclude(e => e.User)
               .ToListAsync();

            foreach (var app in applications)
            {
                Console.WriteLine($"ApplicationId: {app.Id}, EmployeeId: {app.EmployeeId}, Name: {app.Employee?.User?.Name}, Surname: {app.Employee?.User?.Surname}");
            }

            return applications; // Повертаємо вже отриманий список
        }

    }
}
