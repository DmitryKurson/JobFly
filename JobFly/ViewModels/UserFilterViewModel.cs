namespace JobFly.ViewModels
{
    public class UserFilterViewModel
    {
        public string? Search { get; }
        public string? Role { get; }

        public UserFilterViewModel(string? search, string? role = null)
        {
            Search = search;
            Role = role;
        }
    }
}
