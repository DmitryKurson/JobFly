using System.ComponentModel.DataAnnotations;

namespace JobFly.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<VacancyTag> VacancyTags { get; set; }
        public virtual ICollection<EmployeeTag> EmployeeTags { get; set; }
    }
}
