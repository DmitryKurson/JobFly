namespace JobFly.ViewModels
{
    public class VacancySortViewModel
    {
        public VacancySortState IDSort { get; }
        public VacancySortState TitleSort { get; }
        public VacancySortState SalarySort { get; }
        public VacancySortState StatusSort { get; }
        public VacancySortState Current { get; }

        public VacancySortViewModel(VacancySortState sortOrder)
        {
            IDSort = sortOrder == VacancySortState.IdAsc ? VacancySortState.IdDesc : VacancySortState.IdAsc;
            TitleSort = sortOrder == VacancySortState.TitleAsc ? VacancySortState.TitleAsc : VacancySortState.TitleAsc;
            SalarySort = sortOrder == VacancySortState.SalaryAsc ? VacancySortState.SalaryDesc : VacancySortState.SalaryAsc;
            StatusSort = sortOrder == VacancySortState.StatusAsc ? VacancySortState.StatusDesc : VacancySortState.StatusAsc;
            Current = sortOrder;
        }
    }
}
