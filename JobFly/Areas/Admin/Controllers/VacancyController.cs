using JobFly.Areas.Employer.Models;
using JobFly.Areas.Employer.Services;
using JobFly.Data;
using JobFly.Models;
using JobFly.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace JobFly.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class VacancyController : Controller
    {
        private readonly IVacancyService _vacancyService;
        private readonly ICategoryService _categoryService;
        private const int PageSize = 10;

        public VacancyController(IVacancyService vacancyService, ICategoryService categoryService)
        {
            _vacancyService = vacancyService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(string title, int? categoryId, int page = 1, ViewModels.VacancySortState sortOrder = VacancySortState.IdAsc)
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                await _vacancyService.Delete(id);
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
