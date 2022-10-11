namespace SeniorProject.Models
{
    public class Course
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public List<Topics> Topics { get; set; }
        public ArticulationMatrix ArticulationMatrix { get; set; }
        public List<Sections> Sections { get; set; }
        public int Credit { get; set; }

    }
}
