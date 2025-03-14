using System.ComponentModel.DataAnnotations;

namespace JobFly.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Vacancy> Vacancies { get; set; }
    }
}
