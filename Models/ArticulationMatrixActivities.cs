namespace SeniorProject.Models
{
    public class ArticulationMatrixActivities
    {
        public int Id { get; set; }
        public ArticulationMatrix ArticulationMatrix_Ref { get; set; }
        public Activities activity_Ref { get; set; }
        public bool status { get; set; }
    }
}
