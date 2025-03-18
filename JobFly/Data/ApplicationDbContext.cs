using JobFly.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobFly.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<VacancyTag> VacancyTags { get; set; }
        public DbSet<EmployeeTag> EmployeeTags { get; set; }
        public DbSet<Application> Applications { get; set; }

        public Employee EmployeeProfile { get; set; }
        public Employer EmployerProfile { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(450);
            });

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<Employee>(e => e.UserId);

            modelBuilder.Entity<Employer>()
                .HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<Employer>(e => e.UserId);

            modelBuilder.Entity<VacancyTag>()
                .HasKey(vt => new { vt.VacancyId, vt.TagId });

            modelBuilder.Entity<EmployeeTag>()
                .HasKey(et => new { et.EmployeeId, et.TagId });

            modelBuilder.Entity<Vacancy>()
                .HasOne(v => v.Employer)
                .WithMany(e => e.Vacancies)
                .HasForeignKey(v => v.EmployerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vacancy>()
                .HasOne(v => v.Category)
                .WithMany(c => c.Vacancies)
                .HasForeignKey(v => v.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Application>()
                .HasOne(a => a.Vacancy)
                .WithMany(v => v.Applications)
                .HasForeignKey(a => a.VacancyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Application>()
                .HasOne(a => a.Employee)
                .WithMany(e => e.Applications)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
