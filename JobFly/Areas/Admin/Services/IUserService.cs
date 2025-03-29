using JobFly.Models;

namespace JobFly.Areas.Admin.Services
{
    public interface IUserService
    {
        Task<List<(ApplicationUser User, string Role)>> GetUsersWithRolesAsync(string role, string search, int page, int pageSize);
        Task<int> GetUsersCount(string role, string search);
        Task<ApplicationUser?> GetUserById(string id);
    }
}
