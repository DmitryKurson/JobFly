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

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Task Description is required")]
        public string TaskDescription { get; set; }

        [Required(ErrorMessage = "Must to have is required")]
        public string MustToHave { get; set; }

        [Required(ErrorMessage = "Must to have is required")]
        public string GoodToHave { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Salary must be greater than 0")]
        public int Salary { get; set; }
        public bool IsActive { get; set; } // Например, "Открыта", "Закрыта"

        [ForeignKey("Employer")]
        public string EmployerId { get; set; }
        public virtual Employer Employer { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<VacancyTag> VacancyTags { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
    }
}
