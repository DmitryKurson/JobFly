using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobFly.Models
{
    public class Employer
    {
        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string CompanyTitle { get; set; }
        public virtual ICollection<Vacancy> Vacancies { get; set; }
    }
}
