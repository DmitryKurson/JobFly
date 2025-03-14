namespace JobFly.Models
{
    public class VacancyTag
    {
        public int VacancyId { get; set; }
        public int TagId { get; set; }

        public virtual Vacancy Vacancy { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
