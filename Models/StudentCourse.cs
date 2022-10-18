using System.ComponentModel.DataAnnotations;

namespace SeniorProject.Models
{
    public class StudentCourse
    {
        [Key]
        public int StuCourse_Id { get; set; }
        public Student Student_Ref { get; set; }
        public Course course_Ref { get; set; }
        public Session session_Ref { get; set; }
        public double marks { get; set; }
        public string result { get; set; }
    }
}
