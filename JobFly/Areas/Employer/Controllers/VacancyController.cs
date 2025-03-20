using JobFly.Areas.Employer.Services;
using JobFly.Data;
using JobFly.Models;
using JobFly.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobFly.Areas.Employer.Controllers
{
    public class VacancyController : Controller
    {
        private readonly IVacancyService _vacancyService;
        private const int PageSize = 3;

        public VacancyController(IVacancyService projectService)
        {
            _vacancyService = projectService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Vacancy vacancy)
        {
            await _vacancyService.Create(vacancy);
            return RedirectToAction("Index");
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
        public async Task<IActionResult> Update(int? id)
        {
            if (id != null)
            {
                var vacancy = await _vacancyService.GetVacancyById(id);
                if (vacancy != null)
                    return View(vacancy);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(Vacancy vacancy)
        {
            await _vacancyService.Update(vacancy);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index(string title, int page = 1, ViewModels.VacancySortState sortOrder = VacancySortState.IdAsc)
        {
            var projects = await _vacancyService.GetVacancies(title, sortOrder, page, PageSize);
            var count = await _vacancyService.GetVacanciesCount(title);

            VacancyIndexViewModel viewModel = new VacancyIndexViewModel(
                projects,
                new PageViewModel(count, page, PageSize),
                new FilterViewModel(title),
                new VacancySortViewModel(sortOrder)
            );

            return View(viewModel);
        }
    }
}
