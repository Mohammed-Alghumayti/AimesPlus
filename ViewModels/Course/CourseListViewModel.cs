using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SeniorProject.ViewModels
{
    public class CourseListViewModel
    {
        //could be used for search function
        public string Query { get; set; }
        public List<CourseItem> courses { get; set; }

        public class CourseItem
        {
            [Display(Name = "Course ID")]
            public int course_Id { get; set; }
            [Display(Name = "Course Title")]
            public string course_Title { get; set; }
            [Display(Name = "Course Code")]
            public string course_Code { get; set; }
        }
    }
}

    
        
  

