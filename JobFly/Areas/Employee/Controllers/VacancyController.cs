using JobFly.Areas.Employer.Services;
using JobFly.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobFly.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class VacancyController : Controller
    {
        private readonly IVacancyService _vacancyService;
        private readonly ICategoryService _categoryService;
        private const int PageSize = 3;
        public VacancyController(IVacancyService vacancyService, ICategoryService categoryService)
        {
            _vacancyService = vacancyService;
            _categoryService = categoryService;
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

    }
}
