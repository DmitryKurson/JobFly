using JobFly.Models;

namespace JobFly.ViewModels
{
    public class FilterViewModel
    {
        public FilterViewModel(string title, IEnumerable<Category> categories, int? categoryId = null)
        {
            SelectedTitle = title;
            SelectedCategoryId = categoryId;
            Categories = categories;
        }
        public string SelectedTitle { get; }
        public int? SelectedCategoryId { get; }
        public IEnumerable<Category> Categories { get; }  // Список категорій для вибору
    }
}
