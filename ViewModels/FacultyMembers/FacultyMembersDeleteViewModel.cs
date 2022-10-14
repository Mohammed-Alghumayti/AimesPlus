using System.ComponentModel.DataAnnotations;


namespace SeniorProject.ViewModels
{
    public class FacultyMembersDeleteViewModel
    {
        public int Id { get; set; }

        [Required]

        public int AcademicID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }


        [Display(Name = "Role")]
        public string Role { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
