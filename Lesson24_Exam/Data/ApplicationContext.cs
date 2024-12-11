using Lesson24_Exam.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lesson24_Exam.Data
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Event> Events { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Event>().HasMany(e => e.Users).WithMany(e => e.Events);
        }
    }
}
