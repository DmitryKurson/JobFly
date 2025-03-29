using JobFly.Models;

namespace JobFly.ViewModels
{
    public class UserIndexViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; }
        public List<string> Roles { get; }
        public PageViewModel PageViewModel { get; }
        public UserFilterViewModel UserFilterViewModel { get; }

        public UserIndexViewModel(IEnumerable<ApplicationUser> users, List<string> roles, PageViewModel pageViewModel, UserFilterViewModel filterViewModel)
        {
            Users = users;
            Roles = roles;
            PageViewModel = pageViewModel;
            UserFilterViewModel = filterViewModel;
        }
    }
}
