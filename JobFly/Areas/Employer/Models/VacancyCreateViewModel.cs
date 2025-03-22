using JobFly.Models;

namespace JobFly.Areas.Employer.Models
{
    public class VacancyCreateViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public Vacancy Vacancy { get; set; }
    }
}
