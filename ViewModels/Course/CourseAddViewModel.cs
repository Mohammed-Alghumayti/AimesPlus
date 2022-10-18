using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SeniorProject.ViewModels
{
    public class CourseAddViewModel
    {
        [Display(Name = "Course ID")]
        public int course_Id { get; set; }

        [Required]
        [Display(Name = "Course Title")]
        public int CourseTitle { get; set; }

        [Required]
        [MinLength(7)]
        [MaxLength(7)]
        [Display(Name = "Course Code")]
        public string CourseCode{ get; set; }

        
    }
}
