using JobFly.Models;

namespace JobFly.ViewModels
{
    public class VacancyIndexViewModel
    {
        public IEnumerable<Vacancy> vacancies { get; }
        public PageViewModel PageViewModel { get; }
        public FilterViewModel FilterViewModel { get; }
        public VacancySortViewModel VacancySortViewModel { get; }
        public VacancyIndexViewModel(IEnumerable<Vacancy> vacancies, PageViewModel pageViewModel, FilterViewModel filterViewModel, VacancySortViewModel VacancySortViewModel)
        {
            this.vacancies = vacancies;
            PageViewModel = pageViewModel;
            FilterViewModel = filterViewModel;
            this.VacancySortViewModel = VacancySortViewModel;
        }
    }
}
