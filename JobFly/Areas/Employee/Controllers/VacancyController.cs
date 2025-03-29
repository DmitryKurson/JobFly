using JobFly.Areas.Employer.Models;
using JobFly.Areas.Employer.Services;
using JobFly.Models;
using JobFly.Services;
using JobFly.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobFly.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Policy = "EmployeeOnly")]
    public class VacancyController : Controller
    {
        private readonly IVacancyService _vacancyService;
        private readonly ICategoryService _categoryService;
        private readonly IApplicationService _applicationService;
        private const int PageSize = 10;
        public VacancyController(IVacancyService vacancyService, ICategoryService categoryService, IApplicationService applicationService)
        {
            _vacancyService = vacancyService;
            _categoryService = categoryService;
            _applicationService = applicationService;
        }
        public async Task<IActionResult> Index(string title, int? categoryId, int page = 1, VacancySortState sortOrder = VacancySortState.IdAsc)
        {
            var vacancies = await _vacancyService.GetVacancies(title, categoryId, sortOrder, page, PageSize);
            var count = await _vacancyService.GetVacanciesCount(title, categoryId);
            var categories = await _categoryService.GetAll();

            var viewModel = new VacancyIndexViewModel(
                vacancies,
                new PageViewModel(count, page, PageSize),
                new FilterViewModel(title, categories, categoryId),
                new VacancySortViewModel(sortOrder),
                categories
            );

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Details(int vacancyId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized("Користувач не авторизований.");
            }


            var application = new Application
            {
                VacancyId = vacancyId,
                EmployeeId = userId,
                Status = "Not readed",
                DateApplied = DateTime.Now
            };

            await _applicationService.Create(application);

            return RedirectToAction("Index");
        }


    }
}
