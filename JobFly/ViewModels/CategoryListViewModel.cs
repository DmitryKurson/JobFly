using JobFly.Models;

namespace JobFly.ViewModels
{
    public class CategoryListViewModel
    {
        public string? Title { get; set; }
       

        public IEnumerable<Category> categories { get; set; } = new List<Category>();
    }

}

