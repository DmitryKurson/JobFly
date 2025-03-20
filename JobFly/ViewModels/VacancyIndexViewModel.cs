using JobFly.Models;

namespace JobFly.ViewModels
{
    public class VacancyIndexViewModel
    {
        public IEnumerable<Vacancy> vacancies { get; }
        public PageViewModel PageViewModel { get; }
        public FilterViewModel FilterViewModel { get; }
        public VacancySortViewModel ProjectSortViewModel { get; }
        public VacancyIndexViewModel(IEnumerable<Vacancy> vacancies, PageViewModel pageViewModel, FilterViewModel filterViewModel, VacancySortViewModel ProjectSortViewModel)
        {
            this.vacancies = vacancies;
            PageViewModel = pageViewModel;
            FilterViewModel = filterViewModel;
            this.ProjectSortViewModel = ProjectSortViewModel;
        }
    }
}
