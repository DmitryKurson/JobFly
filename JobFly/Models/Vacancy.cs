using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobFly.Models
{
    public class Vacancy
    {
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string TaskDescription { get; set; }
        public string MustToHave { get; set; }
        public string GoodToHave { get; set; }
        public int Salary { get; set; }
        public string Status { get; set; } // Например, "Открыта", "Закрыта"

        [ForeignKey("Employer")]
        public string EmployerId { get; set; }
        public virtual Employer Employer { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<VacancyTag> VacancyTags { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
    }
}
