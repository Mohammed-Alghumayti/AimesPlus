using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SeniorProject.ViewModels
{
    public class CourseDeleteViewModel
    {
        [Display(Name = "Course ID")]
        public int Course_Id { get; set; }

        
        [Display(Name = "Course Title")]
        public string Course_Title { get; set; }

        
        [Display(Name = "Course Code")]
        public string Course_Code{ get; set; }

        
    }
}
