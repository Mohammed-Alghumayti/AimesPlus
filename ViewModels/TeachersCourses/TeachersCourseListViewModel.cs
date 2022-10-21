using SeniorProject.Models;
using System.Collections.Generic;


namespace SeniorProject.ViewModels
{
    public class TeachersCourseListViewModel
    {
        public List<TeachersCoursesItem> teachersCourses { get; set; }

        public class TeachersCoursesItem
        {
            
            public int teacherCourse_Id { get; set; }
            public Course course_Ref { get; set; }
            public Teachers teacher_Ref { get; set; }
            public String SemesterStart { get; set; }
            public String SemesterEnd { get; set; }
        }
    }
}
