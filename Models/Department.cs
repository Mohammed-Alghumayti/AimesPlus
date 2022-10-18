using System.ComponentModel.DataAnnotations;

namespace SeniorProject.Models
{
    public class Department
    {
        [Key]
        public int Dept_Id { get; set; }
        public string Name { get; set; }
        public string description { get; set; }
    }
}
