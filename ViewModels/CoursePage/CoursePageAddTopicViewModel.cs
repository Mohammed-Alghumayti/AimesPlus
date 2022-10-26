using SeniorProject.Models;
using System.ComponentModel.DataAnnotations;

namespace SeniorProject.ViewModels
{
    public class CoursePageAddTopicViewModel
    {

        public int Id { get; set; }
        //string of the course id to be able to pass the id and fetch db later
        public string courseID { get; set; }
        public Course course_Ref { get; set; }
        [Required]
        [Display(Name = "Week No.")]
        public int WeekNo { get; set; }
        [Required]
        [Display(Name = "Topic")]
        public string catalog_topic { get; set; }
        [Required]
        [Display(Name = "Details")]
        public string details { get; set; }

    }
}
