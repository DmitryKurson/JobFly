using JobFly.Models;

namespace JobFly.ViewModels
{
    public class CategoryIndexViewModel
    {
        public IEnumerable<Category> Categories { get; }
        public PageViewModel PageViewModel { get; }
        public FilterViewModel FilterViewModel { get; }
        public CategorySortViewModel CategorySortViewModel { get; }
        public CategoryIndexViewModel(IEnumerable<Category> categories, PageViewModel pageViewModel, FilterViewModel filterViewModel, CategorySortViewModel CategorySortViewModel)
        {
            this.Categories = categories;
            PageViewModel = pageViewModel;
            FilterViewModel = filterViewModel;
            this.CategorySortViewModel = CategorySortViewModel;
        }
    }
}
