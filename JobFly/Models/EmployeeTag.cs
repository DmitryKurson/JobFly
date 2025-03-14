namespace JobFly.Models
{
    public class EmployeeTag
    {
        public string EmployeeId { get; set; }
        public int TagId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Tag Tag { get; set; }
    }

}
