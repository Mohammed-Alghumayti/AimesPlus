using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SeniorProject.Data;
using SeniorProject.Models;
using SeniorProject.ViewModels;
using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.CompilerServices;


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
                return RedirectToAction("Index", "Login");
            }


            //for the home button to access across website
            var academicID = HttpContext.Session.GetString("UserId");

            // get TeacherCourse from database
            var teachersCourses = applicationDbContext.TeachersCourse.Include(t => t.teacher_Ref).Include(c => c.course_Ref)
                .Where(tc => tc.teacher_Ref.AcademicId == x.AcademicID || tc.teacher_Ref.AcademicId == Convert.ToInt64(academicID))
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




        #region List
        // GET: TeacherCourseController
        [HttpGet]
        public IActionResult InstList()
        {
            // get TeacherCourse from database
            var teachersCourses = applicationDbContext.TeachersCourse.Include(t => t.teacher_Ref).Include(c => c.course_Ref).ToList();

            // convert model to view model
            var viewModel = new TeachersCourseListViewModel
            {
                teachersCourses = teachersCourses.Select(t => new TeachersCourseListViewModel.TeachersCoursesItem
                {
                    teacherCourse_Id = t.teacherCourse_Id,
                    teacher_Ref = t.teacher_Ref,
                    course_Ref = t.course_Ref,
                    SemesterStart = t.SemesterStart,
                    SemesterEnd = t.SemesterEnd,
                }).ToList()
            };

            // pass the view model to the view
            return View(viewModel);
        }
        #endregion


        [HttpGet]
        public IActionResult Search(string query)
        {
            var TeachersList = applicationDbContext.TeachersCourse
                .Include(t => t.teacher_Ref)
                .Include(c => c.course_Ref)
                .Where(a => a.teacher_Ref.teacher_Name.Contains(query)||
                            a.teacher_Ref.AcademicId.ToString().Contains(query)||
                            a.course_Ref.course_Code.Contains(query))
                .ToList();

            
                var viewModel = new TeachersCourseListViewModel
                {
                    teachersCourses = TeachersList.Select(a => new TeachersCourseListViewModel.TeachersCoursesItem
                    {
                        SemesterEnd = a.SemesterEnd,
                        SemesterStart = a.SemesterStart,
                        course_Ref = a.course_Ref,
                        teacherCourse_Id = a.teacherCourse_Id,
                        teacher_Ref = a.teacher_Ref
                    }).ToList()
                };

                return View("InstList", viewModel);
           
            
        }



    }
}