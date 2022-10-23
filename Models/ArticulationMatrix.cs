using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorProject.Models
{
    public class ArticulationMatrix
    {
        [Key]
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
