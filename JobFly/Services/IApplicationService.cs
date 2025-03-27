using JobFly.Models;
using JobFly.ViewModels;

namespace JobFly.Services
{
    public interface IApplicationService
    {
        //Task<IEnumerable<Application>> GetApplications(string? title, CategorySortState sortOrder, int page, int pageSize);
        //Task<IEnumerable<Application>> GetAll();
        //Task<int> GetApplicationCount(string? title);
        Task<Application?> GetApplicationById(int? id);
        Task Create(Application application);
        //Task Update(Application application);
        Task Delete(int? id);
        //Task<List<Application>> GetApplicationsForVacancy(int vacancyId);
        //Task<List<Application>> GetApplicationsForEmployer(string employerId, string title, int? categoryId, ApplicationSortState sortOrder, int page, int pageSize);
        //Task<int> GetApplicationsCountForEmployer(string employerId, string? title, int? categoryId);

    }
}
