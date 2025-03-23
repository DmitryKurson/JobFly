using System.ComponentModel.DataAnnotations;

namespace JobFly.Areas.Employer.Models
{
    public class VacancyUpdateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string TaskDescription { get; set; }

        [Required]
        public string MustToHave { get; set; }

        [Required]
        public string GoodToHave { get; set; }

        [Required]
        [Range(1, 100000)]
        public int Salary { get; set; }

        public bool IsActive { get; set; }
    }
}
