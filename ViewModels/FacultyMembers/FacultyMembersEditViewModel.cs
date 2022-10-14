using System.ComponentModel.DataAnnotations;


namespace SeniorProject.ViewModels
{
    public class FacultyMembersEditViewModel
    {
        public int Id { get; set; }
        
        [Required]
        // min and max
        public int AcademicID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

       
        [Display(Name = "Role")]
        public string Role { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
