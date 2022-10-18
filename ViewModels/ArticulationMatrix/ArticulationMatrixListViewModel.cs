using SeniorProject.Models;
using static SeniorProject.ViewModels.CourseListViewModel;
using System.Collections.Generic;



namespace SeniorProject.ViewModels.ArticulationMatrix
{
    public class ArticulationMatrixListViewModel
    {
        public string Query { get; set; }
        public List<ArticulationMatrixItim> ArticulationMatrix { get; set; }

        public class ArticulationMatrixItim
        {
            public int Id { get; set; }
            public TeachersCourse teacherCourse_Ref { get; set; }
            public string CLO { get; set; }
            public LOD LOD_Ref { get; set; }
            public int SO { get; set; }
            public bool Assessing_SO { get; set; }
        }
       
    }
}
