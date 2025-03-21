using JobFly.Models;

namespace JobFly.ViewModels
{
    public class VacancyListViewModel
    {
        public string? Title { get; set; }
        public string? TaskDescription { get; set; }
        public string? MustToHave { get; set; }
        public string? GoodToHave { get; set; }

        public IEnumerable<Vacancy> projects { get; set; } = new List<Vacancy>();
    }

}

