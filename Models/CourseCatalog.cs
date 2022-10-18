using System.ComponentModel.DataAnnotations;

namespace SeniorProject.Models
{
    public class CourseCatalog
    {
        public int Id { get; set; }
        public Course course_Ref { get; set; }
        public string catalog_topic { get; set; }    
        public string details { get; set; }
    }
}
