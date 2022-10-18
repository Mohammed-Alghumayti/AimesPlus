using System.ComponentModel.DataAnnotations;

namespace SeniorProject.Models
{
    public class Course
    {
        [Key]
        public int course_Id { get; set; }
        public string course_Title { get; set; }
        public string course_Code { get; set; }
       
    }
}
