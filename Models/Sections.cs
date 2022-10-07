namespace SeniorProject.Models
{
    public class Sections
    {
        public int Id { get; set; }
        public Students students { get; set; }
        public Instructors instructors { get; set; }
        public string code { get; set; }
        public string semester { get; set; }


    }
}
