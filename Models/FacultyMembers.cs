namespace FinalProject.Models
{
    public class FacultyMembers
    {
    public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public Course? Courses { get; set; }
        // course is nullable in case of adminstrator

    }
}
