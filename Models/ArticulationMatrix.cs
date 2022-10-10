namespace SeniorProject.Models
{
    public class ArticulationMatrix
    {
        public int Id { get; set; }
        public string CLOs { get; set; }
        public LOD LOD { get; set; }
        public SO SO { get; set; }
        public Boolean AssesingSO { get; set; }
        public Activites? Activites { get; set; }
        public AssesmentTools? AssesmentTools { get; set; }
    }
}
