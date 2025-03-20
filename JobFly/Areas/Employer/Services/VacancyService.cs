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
                VacancySortState.StatusAsc => vacancies.OrderBy(s => s.Status),
                VacancySortState.StatusDesc => vacancies.OrderByDescending(s => s.Status),
                _ => vacancies.OrderBy(s => s.Id),
            };

            return await vacancies.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetVacanciesCountAsync(string? title)
        {
            IQueryable<Vacancy> projects = _db.Vacancies;

            if (!string.IsNullOrEmpty(title))
            {
                projects = projects.Where(p => p.Title!.Contains(title));
            }

            return await projects.CountAsync();
        }

        public async Task<Vacancy?> GetVacancyById(int id)
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

        public async Task Delete(int id)
        {
            var project = await GetVacancyById(id);
            if (project != null)
            {
                _db.Vacancies.Remove(project);
                await _db.SaveChangesAsync();
            }
        }
    }
}

