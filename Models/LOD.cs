
using System.ComponentModel.DataAnnotations;

namespace SeniorProject.Models
{
    public class LOD
    {
        [Key]
        public int LOD_Id { get; set; }
        public string LOD_Name { get; set; }
        public string LOD_Description { get; set; }
    }
}
