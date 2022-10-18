namespace SeniorProject.Models
{
    public class StudentCourseActivities
    {
        public int Id { get; set; }
        public StudentCourse StuCourse_Ref { get; set; }
        public Activities Activities_Ref { get; set; }
        public string remarks { get; set; }
        public string result { get; set; }
        public string date { get; set; }

    }
}
