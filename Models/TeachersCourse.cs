using System.ComponentModel.DataAnnotations;

namespace SeniorProject.Models
{
    public class TeachersCourse
    {
        [Key]
        public int teacherCourse_Id { get; set; }
        public Course course_Ref { get; set; }
        public Teachers teacher_Ref { get; set; }
        public String SemesterStart { get; set; }
        public String SemesterEnd { get; set; }
    }
}
