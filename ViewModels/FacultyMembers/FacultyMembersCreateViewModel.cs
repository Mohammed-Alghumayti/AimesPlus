using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SeniorProject.ViewModels
{
    public class FacultyMembersCreateViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(6)]
        public int AcademicID { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }
        
        [Display(Name = "Password")]
        public string Password { get; set; }
        
    }
}
