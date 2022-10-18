using System.ComponentModel.DataAnnotations;


namespace SeniorProject.Models
{
    public class AssessmentTools
    {
        [Key]
        public int toolId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
