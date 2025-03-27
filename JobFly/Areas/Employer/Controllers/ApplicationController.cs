using JobFly.Areas.Employer.Services;
using JobFly.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobFly.Areas.Employer.Controllers
{
    [Area("Employer")]
    public class ApplicationController : Controller
    {
        private readonly IVacancyService _vacancyService;
        private readonly ICategoryService _categoryService;
        private const int PageSize = 10;

        public ApplicationController(IVacancyService vacancyService, ICategoryService categoryService)
        {
            _vacancyService = vacancyService;
            _categoryService = categoryService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                await _vacancyService.Delete(id);
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [Authorize]
        public async Task<IActionResult> Index(string title, int page = 1, int? categoryId = null,
    ViewModels.VacancySortState sortOrder = VacancySortState.IdAsc)
        {
            var employerId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var vacancies = await _vacancyService.GetVacanciesForEmployer(employerId, title, categoryId, sortOrder, page, PageSize);
            var count = await _vacancyService.GetVacanciesCountForEmployer(employerId, title, categoryId);

            // Отримуємо список всіх категорій
            var categories = await _categoryService.GetAll();

            VacancyIndexViewModel viewModel = new VacancyIndexViewModel(
                vacancies,
                new PageViewModel(count, page, PageSize),
                new FilterViewModel(title, categories, categoryId),
                new VacancySortViewModel(sortOrder),
                categories // Передаємо список категорій
            );

            return View(viewModel);
        }
    }
}
