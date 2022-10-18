using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorProject.Models
{
    public class ArticulationMatrix
    {
        [Key]
        public int Id { get; set; }
        public TeachersCourse teacherCourse_Ref { get; set; }
        public string CLO { get; set; }
        public LOD LOD_Ref { get; set; }
        public int SO { get; set; }
        public bool Assessing_SO { get; set; }
    }
}
