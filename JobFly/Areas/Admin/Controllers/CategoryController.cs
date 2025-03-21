﻿using JobFly.Areas.Employer.Models;
using JobFly.Areas.Employer.Services;
using JobFly.Models;
using JobFly.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobFly.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private const int PageSize = 3;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {   
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Category category)
        {      
            await _categoryService.Create(category);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                await _categoryService.Delete(id);
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [Authorize]
        public async Task<IActionResult> Update(int? id)
        {
            if (id != null)
            {
                var category = await _categoryService.GetCategoryById(id);
                if (category != null)
                    return View(category);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(Category category)
        {
            await _categoryService.Update(category);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index(string title, int page = 1, ViewModels.CategorySortState sortOrder = CategorySortState.IdAsc)
        {
            var vacancies = await _categoryService.GetCategories(title, sortOrder, page, PageSize);
            var count = await _categoryService.GetCategoriesCount(title);

            CategoryIndexViewModel viewModel = new CategoryIndexViewModel(
                vacancies,
                new PageViewModel(count, page, PageSize),
                new FilterViewModel(title),
                new CategorySortViewModel(sortOrder)
            );

            return View(viewModel);
        }

        //[Authorize]
        //public async Task<IActionResult> Index(string title, int page = 1, ViewModels.VacancySortState sortOrder = VacancySortState.IdAsc)
        //{
        //    var employerId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        //    var vacancies = await _categoryService.GetVacanciesForEmployer(employerId, title, sortOrder, page, PageSize);
        //    var count = await _categoryService.GetVacanciesCountForEmployer(employerId, title);

        //    VacancyIndexViewModel viewModel = new VacancyIndexViewModel(
        //        vacancies,
        //        new PageViewModel(count, page, PageSize),
        //        new FilterViewModel(title),
        //        new VacancySortViewModel(sortOrder)
        //    );

        //    return View(viewModel);
        //}

    }
}
