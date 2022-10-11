namespace SeniorProject.Models
{
    public class Sections
    {
        public int Id { get; set; }
        public List<Students> students { get; set; }
        public List<Instructors> instructors { get; set; }
        public string code { get; set; }
        public string semester { get; set; }


    }
}
