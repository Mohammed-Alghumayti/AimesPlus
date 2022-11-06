namespace SeniorProject.Models
{
    public class GradeDistribution
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
