namespace JobFly.ViewModels
{
    public class FilterViewModel
    {
        public FilterViewModel(string title)
        {
            SelectedTitle = title;
        }
        public string SelectedTitle { get; }
    }
}
