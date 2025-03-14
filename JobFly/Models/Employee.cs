using static System.Net.Mime.MediaTypeNames;

namespace JobFly.Models
{
    public class Employee : ApplicationUser
    {
        public string ResumeText { get; set; }
        public virtual ICollection<EmployeeTag> EmployeeTags { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
    }
}
