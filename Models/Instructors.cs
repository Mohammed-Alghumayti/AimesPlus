namespace SeniorProject.Models
{
    public class Instructors
    {
        public int Id { get; set; }
        public int AcademicID { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public List<Course>? Courses { get; set; }
        
    }
}
