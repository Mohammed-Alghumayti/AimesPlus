using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeniorProject.Data;
using SeniorProject.Models;
using SeniorProject.ViewModels;

namespace SeniorProject.Controllers
{
    public class HomeController : Controller
    {
        //database object to use throughout this controller
        private readonly ApplicationDbContext applicationDbContext;
        public HomeController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public IActionResult Index(FacultyMembers x)
        {

            if (!LoginController.CheckAuth(HttpContext))
            {
                return RedirectToAction("Index", "Login") ;
            }


            // get TeacherCourse from database
            var teachersCourses = applicationDbContext.TeachersCourse.Include(t => t.teacher_Ref).Include(c => c.course_Ref)
                .Where(tc => tc.teacher_Ref.AcademicId == x.AcademicID)
                .ToList();

            

            

            // convert model to view model
            var viewModel = new TeachersCourseListViewModel
            {
                teachersCourses = teachersCourses.Select(t => new TeachersCourseListViewModel.TeachersCoursesItem
                {

                    teacher_Ref = t.teacher_Ref,
                    course_Ref = t.course_Ref,
                    SemesterStart = t.SemesterStart,
                    SemesterEnd = t.SemesterEnd,
                }).ToList()
            };


            
            //pass the name to the view
            var name = TempData["name"];
            ViewData["thisdata"] = name; 

            // pass the view model to the view          
            return View(viewModel);
        }
        
        public IActionResult Artic()
        {
            return View("Views/ArticulationMatrix/Index.cshtml");
        }






       

    }
}