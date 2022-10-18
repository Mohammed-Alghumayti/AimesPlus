using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SeniorProject.ViewModels
{
    public class CourseEditViewModel
    {
        [Display(Name = "Course ID")]
        public int Course_Id { get; set; }

        [Required]
        [Display(Name = "Course Title")]
        public string Course_Title { get; set; }

        [Required]
        [MinLength(7)]
        [MaxLength(7)]
        [Display(Name = "Course Code")]
        public string Course_Code{ get; set; }

        
    }
}
