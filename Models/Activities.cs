
using System.ComponentModel.DataAnnotations;

namespace SeniorProject.Models
{
    public class Activities
    {
        [Key]
        public int Activities_Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string activity_Type { get; set; }
    }
}
