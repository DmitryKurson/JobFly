namespace JobFly.Models
{
    public class Employer : ApplicationUser
    {
        public string CompanyTitle { get; set; }
        public virtual ICollection<Vacancy> Vacancies { get; set; }
    }
}
