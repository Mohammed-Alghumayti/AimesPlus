
using System.ComponentModel.DataAnnotations;

namespace SeniorProject.Models
{
    public class Teachers
    {
        [Key]
        public int teacher_Id { get; set; }
        public string teacher_Name { get; set; }
    }
}
