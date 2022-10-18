using SeniorProject.Models;
using Microsoft.EntityFrameworkCore;

namespace SeniorProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<ArticulationMatrix> ArticulationMatrix { get; set; }
        public DbSet<FacultyMembers> FacultyMembers { get; set; }
        public DbSet<ArticulationMatrixActivities> ArticulationMatrixActivities { get; set; }
        public DbSet<Activities> Activities { get; set; }
        public DbSet<AssessmentTools> AssessmentTools { get; set; }
        public DbSet<CourseCatalog> CourseCatalog { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<GradeDistribution> GradeDistribution { get; set; }
        public DbSet<LOD> LOD { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<StudentCourse> StudentCourse { get; set; }
        public DbSet<StudentCourseActivities> StudentCourseActivities { get; set; }
        public DbSet<StudentCourseAssessmentTools> StudentCourseAssessmentTools { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<TeachersCourse> TeachersCourse { get; set; }
        public DbSet<TeachingSchedule> TeachingSchedule { get; set; }

       

        
    }
}
