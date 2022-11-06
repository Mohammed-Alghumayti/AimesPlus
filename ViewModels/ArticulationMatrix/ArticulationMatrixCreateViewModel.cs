using SeniorProject.Models;

namespace SeniorProject.Models
{
    public class ArticulationMatrixCreateViewModel
    {

        public string CLO { get; set; }
        public int SO { get; set; }
        public int LOD { get; set; }
        public bool assessingSO { get; set; }
        public List<ArticulationMatrixAssessmentTools> AssessmentTools { get; set; }
        public List<ArticulationMatrixActivities> Activities { get; set; }


    }
}
