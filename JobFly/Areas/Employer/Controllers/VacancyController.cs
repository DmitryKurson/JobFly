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

namespace JobFly.Areas.Employer.Controllers
{
    [Area("Employer")]
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

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var categories = _categoryService.GetAll();
            var viewModel = new VacancyCreateViewModel
            {
                Vacancy = new Vacancy(),
                Categories = await categories
            };
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Vacancy vacancy)
        {
            vacancy.EmployerId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            vacancy.IsActive = true;
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();

            var vacancy = await _vacancyService.GetVacancyById(id.Value);
            if (vacancy == null)
                return NotFound();

            // Мапимо Vacancy в ViewModel
            var viewModel = new VacancyUpdateViewModel
            {
                Id = vacancy.Id,
                Title = vacancy.Title,
                TaskDescription = vacancy.TaskDescription,
                MustToHave = vacancy.MustToHave,
                GoodToHave = vacancy.GoodToHave,
                Salary = vacancy.Salary,
                IsActive = vacancy.IsActive
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(VacancyUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingVacancy = await _vacancyService.GetVacancyById(model.Id);
            if (existingVacancy == null)
                return NotFound();

            // Оновлюємо поля
            existingVacancy.Title = model.Title;
            existingVacancy.TaskDescription = model.TaskDescription;
            existingVacancy.MustToHave = model.MustToHave;
            existingVacancy.GoodToHave = model.GoodToHave;
            existingVacancy.Salary = model.Salary;
            existingVacancy.IsActive = model.IsActive;

            await _vacancyService.Update(existingVacancy);

            return RedirectToAction("Index");
        }

        //public async Task<IActionResult> Index(string title, int page = 1, ViewModels.VacancySortState sortOrder = VacancySortState.IdAsc)
        //{
        //    var vacancies = await _vacancyService.GetVacancies(title, sortOrder, page, PageSize);
        //    var count = await _vacancyService.GetVacanciesCount(title);

        //    VacancyIndexViewModel viewModel = new VacancyIndexViewModel(
        //        vacancies,
        //        new PageViewModel(count, page, PageSize),
        //        new FilterViewModel(title),
        //        new VacancySortViewModel(sortOrder)
        //    );

        //    return View(viewModel);
        //}

        [Authorize]
        public async Task<IActionResult> Index(string title, int page = 1, ViewModels.VacancySortState sortOrder = VacancySortState.IdAsc)
        {
            var employerId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var vacancies = await _vacancyService.GetVacanciesForEmployer(employerId, title, sortOrder, page, PageSize);
            var count = await _vacancyService.GetVacanciesCountForEmployer(employerId, title);

            VacancyIndexViewModel viewModel = new VacancyIndexViewModel(
                vacancies,
                new PageViewModel(count, page, PageSize),
                new FilterViewModel(title),
                new VacancySortViewModel(sortOrder)
            );

            return View(viewModel);
        }
    }
}
