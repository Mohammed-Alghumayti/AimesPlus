namespace SeniorProject.Models
{
    public class StudentCourseAssessmentTools
    {
        public int Id { get; set; }
        public StudentCourse StuCourse_Ref { get; set; }
        public AssessmentTools tool_Ref { get; set; }
        public double marks { get; set; }
        public string remarks { get; set; }
        public string result { get; set; }
        public string date { get; set; }
    }
}
