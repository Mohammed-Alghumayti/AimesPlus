using System.ComponentModel.DataAnnotations;

namespace SeniorProject.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string email { get; set; }
    }
}
