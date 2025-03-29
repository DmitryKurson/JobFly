using JobFly.Areas.Admin.Services;
using JobFly.Models;
using JobFly.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobFly.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private const int PageSize = 3;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index(string role, string search, int page = 1)
        {
            var usersWithRoles = await _userService.GetUsersWithRolesAsync(role, search, page, PageSize);
            var count = await _userService.GetUsersCount(role, search);

            var viewModel = new UserIndexViewModel(
                usersWithRoles.Select(u => u.User),
                usersWithRoles.Select(u => u.Role).ToList(),
                new PageViewModel(count, page, PageSize),
                new UserFilterViewModel(search, role)
            );

            return View(viewModel);
        }

    }
}
