using JobFly.Models;
using JobFly.ViewModels;

public class VacancyIndexViewModel
{
    public IEnumerable<Vacancy> Vacancies { get; }
    public PageViewModel PageViewModel { get; }
    public FilterViewModel FilterViewModel { get; }
    public VacancySortViewModel VacancySortViewModel { get; }
    public IEnumerable<Category> Categories { get; }

    public VacancyIndexViewModel(IEnumerable<Vacancy> vacancies, PageViewModel pageViewModel,
                                 FilterViewModel filterViewModel, VacancySortViewModel sortViewModel,
                                 IEnumerable<Category> categories)
    {
        Vacancies = vacancies;
        PageViewModel = pageViewModel;
        FilterViewModel = filterViewModel;
        VacancySortViewModel = sortViewModel;
        Categories = categories;
    }
}

