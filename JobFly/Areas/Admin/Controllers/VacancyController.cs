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
    public class VacancyController : Controller
    {
        private readonly IVacancyService _vacancyService;
        private const int PageSize = 3;

        public VacancyController(IVacancyService vacancyService, ICategoryService categoryService)
        {
            _vacancyService = vacancyService;           
        }

        public async Task<IActionResult> Index(string title, int page = 1, ViewModels.VacancySortState sortOrder = VacancySortState.IdAsc)
        {
            var vacancies = await _vacancyService.GetVacancies(title, sortOrder, page, PageSize);
            var count = await _vacancyService.GetVacanciesCount(title);

            VacancyIndexViewModel viewModel = new VacancyIndexViewModel(
                vacancies,
                new PageViewModel(count, page, PageSize),
                new FilterViewModel(title),
                new VacancySortViewModel(sortOrder)
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
