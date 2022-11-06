using System.ComponentModel.DataAnnotations;

namespace SeniorProject.Models
{
    public class ArticulationMatrixAssessmentTools
    {
        [Key]
        public int Id { get; set; }
        public ArticulationMatrix ArticulationMatrix_Ref { get; set; }
        public AssessmentTools AssessmentTools_Ref { get; set; }
        public int WeekNo { get; set; }

    }
}
