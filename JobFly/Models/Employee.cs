using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace JobFly.Models
{
    public class Employee
    {
        public string ResumeText { get; set; }
        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public virtual ICollection<EmployeeTag> EmployeeTags { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
    }
}
