using System.ComponentModel.DataAnnotations;
using JobFly.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JobFly.Areas.Employer.Models
{
    public class VacancyCreateViewModel
    {
        [BindNever]
        public IEnumerable<Category> Categories { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Task Description is required")]
        public string TaskDescription { get; set; }

        [Required(ErrorMessage = "Must to have is required")]
        public string MustToHave { get; set; }

        [Required(ErrorMessage = "Good to have is required")]
        public string GoodToHave { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Salary must be greater than 0")]
        public int Salary { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int? CategoryId { get; set; }

        public VacancyCreateViewModel()
        {
            Categories = Enumerable.Empty<Category>();
        }
    }
}
