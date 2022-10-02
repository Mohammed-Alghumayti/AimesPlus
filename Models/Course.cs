namespace FinalProject.Models
{
    public class Course
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public Topics Topics { get; set; }
        public CLO CLO { get; set; }
        public LOD LOD { get; set; }
        public SO SO { get; set; }
        public Boolean AssesSO { get; set; }
        public InClassAct InClass { get; set; }
        public OutClassAct OutClass { get; set; }
        public AssesmentTools AssesTools { get; set; }
        public Sections Sections { get; set; }
        public int Credit { get; set; }

    }
}
