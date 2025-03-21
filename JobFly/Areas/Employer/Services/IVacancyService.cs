using JobFly.Models;
using JobFly.ViewModels;

namespace JobFly.Areas.Employer.Services
{
    public interface IVacancyService
    {
        Task<IEnumerable<Vacancy>> GetVacancies(string? title, VacancySortState sortOrder, int page, int pageSize);
        Task<int> GetVacanciesCount(string? title);
        Task<Vacancy?> GetVacancyById(int? id);
        public Task<IEnumerable<Vacancy>> GetVacanciesForEmployer(string employerId, string? title, VacancySortState sortOrder, int page, int pageSize);
        public Task<int> GetVacanciesCountForEmployer(string employerId, string? title);
        Task Create(Vacancy project);
        Task Update(Vacancy project);
        Task Delete(int? id);
    }
}
