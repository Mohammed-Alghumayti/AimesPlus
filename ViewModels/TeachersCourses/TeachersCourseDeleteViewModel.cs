using SeniorProject.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace SeniorProject.ViewModels
{
    public class TeachersCourseDeleteViewModel
    {
        
        public int teacherCourse_Id { get; set; }

        public Course course_Ref { get; set; }   
        
        public Teachers teacher_Ref { get; set; }

        public String SemesterStart { get; set; }

        public String SemesterEnd { get; set; }

    }
}
