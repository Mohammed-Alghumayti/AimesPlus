using Microsoft.AspNetCore.Mvc.Rendering;
using SeniorProject.Models;
using System.Collections.Generic;


namespace SeniorProject.ViewModels
{
    public class TeachersCourseCreateViewModel
    {
        
  
            public int teacherCourse_Id { get; set; }
            public Course course_Ref { get; set; }
            public List<SelectListItem> CoursesList { get; set; }
            public Teachers teacher_Ref { get; set; }
            public List<SelectListItem> TeachersList { get; set; }
            public String SemesterStart { get; set; }
            public String SemesterEnd { get; set; }
        
    }
}
