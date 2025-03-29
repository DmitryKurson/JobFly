using JobFly.Data;
using JobFly.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobFly.Areas.Admin.Services
{
    public class UserService:IUserService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<List<(ApplicationUser User, string Role)>> GetUsersWithRolesAsync(string role, string search, int page, int pageSize)
        {
            var query = _userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(u => u.Name.Contains(search) || u.Surname.Contains(search) || u.Email.Contains(search));
            }

            var users = await query
                .OrderBy(u => u.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var usersWithRoles = new List<(ApplicationUser, string)>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userRole = roles.FirstOrDefault() ?? "Unknown";

                // Если передана роль, фильтруем
                if (string.IsNullOrEmpty(role) || userRole == role)
                {
                    usersWithRoles.Add((user, userRole));
                }
            }

            return usersWithRoles;
        }

        public async Task<int> GetUsersCount(string role, string search)
        {
            var query = _userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(u => u.Name.Contains(search) || u.Surname.Contains(search) || u.Email.Contains(search));
            }

            var users = await query.ToListAsync();
            int count = 0;

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userRole = roles.FirstOrDefault() ?? "Unknown";

                if (string.IsNullOrEmpty(role) || userRole == role)
                {
                    count++;
                }
            }

            return count;
        }


        public async Task<ApplicationUser?> GetUserById(string id)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
