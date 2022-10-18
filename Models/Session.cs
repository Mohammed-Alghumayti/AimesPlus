using System.ComponentModel.DataAnnotations;

namespace SeniorProject.Models
{
    public class Session
    {
        [Key]
        public int session_Id { get; set; }
        public string title { get; set; }
        public Department Dept_Ref { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string description { get; set; }


    }
}
