using JobFly.Models;
using JobFly.ViewModels;

namespace JobFly.Areas.Employer.Services
{
    public interface IVacancyService
    {
        Task<IEnumerable<Vacancy>> GetVacancies(string title, int? categoryId, VacancySortState sortOrder, int page, int pageSize);
        Task<int> GetVacanciesCount(string title, int? categoryId);
        Task<Vacancy?> GetVacancyById(int id);
        Task Create(Vacancy vacancy);
        Task Update(Vacancy vacancy);
        Task Delete(int? id);
        Task<List<Vacancy>> GetVacanciesForEmployer(string employerId, string title, int? categoryId, VacancySortState sortOrder, int page, int pageSize);
        Task<int> GetVacanciesCountForEmployer(string employerId, string? title, int? categoryId);

    }
}
