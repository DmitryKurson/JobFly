namespace JobFly.ViewModels
{
    public class CategorySortViewModel
    {
        public CategorySortState IDSort { get; }
        public CategorySortState TitleSort { get; }
        public CategorySortState Current { get; }

        public CategorySortViewModel(CategorySortState sortOrder)
        {
            IDSort = sortOrder == CategorySortState.IdAsc ? CategorySortState.IdDesc : CategorySortState.IdAsc;
            TitleSort = sortOrder == CategorySortState.TitleAsc ? CategorySortState.TitleAsc : CategorySortState.TitleAsc;
            Current = sortOrder;
        }
    }
}
