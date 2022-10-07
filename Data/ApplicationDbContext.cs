using SeniorProject.Models;
using Microsoft.EntityFrameworkCore;

namespace SeniorProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<FacultyMembers> FacultyMembers { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<Instructors> Instructors { get; set; }
        public DbSet<Sections> sections { get; set; }
        public DbSet<CLO> CLO { get; set; }
        public DbSet<SO> SO { get; set; }
        public DbSet<LOD> LOD { get; set; }
        public DbSet<Topics> Topics { get; set; }
        public DbSet<InClassAct> inClassActs { get; set; }
        public DbSet<OutClassAct> OutClassActs { get; set; }
        public DbSet<AssesmentTools> AssesmentTools { get; set; }

        
    }
}
