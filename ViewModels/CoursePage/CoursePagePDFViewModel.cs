using SeniorProject.Models;

namespace SeniorProject.ViewModels
{
    public class CoursePagePDFViewModel
    {

        public string exam { get; set; }
        public List<ArticulationMatrixItem> Articulation { get; set; }
        public List<GradeDistributionItem> GradeDist { get; set; }

        //-----------------Class items

        public class ArticulationMatrixItem
        {
            public int Id { get; set; }
            public int? articNum { get; set; }
            public Course course_Ref { get; set; }
            public string CLO { get; set; }
            public LOD LOD_Ref { get; set; }
            public int SO { get; set; }
            public bool Assessing_SO { get; set; }
            public List<ArticulationMatrixAssessmentTools> AssessmentTools { get; set; }
            public List<ArticulationMatrixActivities> Activities { get; set; }

        }


        public class GradeDistributionItem
        {
            public int Id { get; set; }

            public string coursecode { get; set; }
            public string Assessment { get; set; }
            public int? week_Number { get; set; }
            public int? SO1 { get; set; }
            public int? SO2 { get; set; }
            public int? SO3 { get; set; }
            public int? SO4 { get; set; }
            public int? SO5 { get; set; }
            public int? SO6 { get; set; }
            public double? percentage { get; set; }
            public bool? assessing_SO { get; set; }
            public int? SOchoice { get; set; }

        }


    }
}
