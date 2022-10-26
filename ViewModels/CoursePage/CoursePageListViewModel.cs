using SeniorProject.Models;

namespace SeniorProject.ViewModels
{
    public class CoursePageListViewModel
    {

        public Course Course { get; set; }
        public List<CourseCatalogItem> catalog { get; set; }    
        public List<ArticulationMatrixItem> Articulation { get; set; }

        //-----------------Class items
        public class CourseCatalogItem
        {
            public int Id { get; set; }
            public Course course_Ref { get; set; }
            public int WeekNo { get; set; }
            public string catalog_topic { get; set; }
            public string details { get; set; }
        }
        public class ArticulationMatrixItem
        {
            public int Id { get; set; }
            public Course course_Ref { get; set; }
            public string CLO { get; set; }
            public LOD LOD_Ref { get; set; }
            public int SO { get; set; }
            public bool Assessing_SO { get; set; }
            public List<ArticulationMatrixAssessmentTools> AssessmentTools { get; set; }
            public List<ArticulationMatrixActivities> Activities { get; set; }
        }

        }
}
