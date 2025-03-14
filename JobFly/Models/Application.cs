using System.ComponentModel.DataAnnotations;

namespace JobFly.Models
{
    public class Application
    {
        [Key]
        public int Id { get; set; }

        public int VacancyId { get; set; }
        public string EmployeeId { get; set; }
        public string Status { get; set; } // Например, "На рассмотрении", "Отклонено", "Принято"
        public DateTime DateApplied { get; set; }

        public virtual Vacancy Vacancy { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
