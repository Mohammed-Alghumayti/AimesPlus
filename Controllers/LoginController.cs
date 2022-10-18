using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SeniorProject.Data;
using SeniorProject.Models;
using SeniorProject.ViewModels;
using System.Linq;

namespace SeniorProject.Controllers
{
    public class LoginController : Controller
    {

        private readonly ApplicationDbContext applicationDbContext;

        public LoginController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        // GET: LoginController
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Instruct()
        {
            return View();
        }


        //----------------------------------

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult check(FacultyMembers member)
        {
            // checking if info match db
            var currentMember = applicationDbContext.FacultyMembers.FirstOrDefault(
                x => x.AcademicID == member.AcademicID && x.Password == member.Password);

            //checking role
            //var roleCheck = applicationDbContext.FacultyMembers.FirstOrDefault(x => x.Role == "admin");


            if (currentMember != null)
            {
                if (currentMember.Role == "Admin")
                {

                    return View("AdminHome");

                }
                else
                {
                    ViewData["thisdata"] = currentMember;
                    return View("/Views/Home/Index.cshtml");
                }
            }
            else
            { TempData["Message"] = "Wrong Password or id"; }
            return RedirectToAction("Index");
        }



        // making a list for the adminstrator

        [HttpGet]
        public IActionResult AdminList()
        {
            var FacultyMembers = applicationDbContext.FacultyMembers.ToList();

            var viewModel = new FacultyMembersListViewModel
            {
                FacultyMembers = FacultyMembers.Select(a => new FacultyMembersListViewModel.FacultyMembersItem
                {
                    Id = a.Id,
                    AcademicID = a.AcademicID,
                    Name = a.Name,
                    Role = a.Role
                }).ToList()
            };

            return View(viewModel);
        }

        //=================================================================================

        //Create a new user(faculty member) to the system from adminstration page

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(FacultyMembers viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var member = new FacultyMembers
            {
                AcademicID = viewModel.AcademicID,
                Name = viewModel.Name,
                Password = viewModel.Password,
                Role = viewModel.Role,

            };

            applicationDbContext.FacultyMembers.Add(member);
            applicationDbContext.SaveChanges();

            return RedirectToAction("AdminList");
        }

        //=================================================================================


        //------------Edit----------------------
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var facultyMembers = applicationDbContext.FacultyMembers
                .FirstOrDefault(a => a.Id == id);

            var viewModel = new FacultyMembersEditViewModel
            {
                Id = facultyMembers.Id,
                AcademicID = facultyMembers.AcademicID,
                Role = facultyMembers.Role,
                Password = facultyMembers.Password,
                Name = facultyMembers.Name
            };

            return View(viewModel);
        }
        //-----------------------------------

        [HttpPost]
        public IActionResult EditPost(FacultyMembersEditViewModel viewModel)
        {


            var facultyMembers = new FacultyMembers
            {
                Id = viewModel.Id,
                AcademicID = viewModel.AcademicID,
                Role = viewModel.Role,
                Password = viewModel.Password,
                Name = viewModel.Name
            };

            applicationDbContext.FacultyMembers.Update(facultyMembers);
            applicationDbContext.SaveChanges();

            return RedirectToAction("AdminList");
        }

        //=================================================================================

        //-----------Delete----------------

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var facultyMembers = applicationDbContext.FacultyMembers
                .FirstOrDefault(a => a.Id == id);

            var viewModel = new FacultyMembersDeleteViewModel
            {
                Id = facultyMembers.Id,
                AcademicID = facultyMembers.AcademicID,
                Role = facultyMembers.Role,
                Password = facultyMembers.Password,
                Name = facultyMembers.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DeletePost(int id)
        {
            var facultyMembers = applicationDbContext.FacultyMembers
                .FirstOrDefault(a => a.Id == id);

            applicationDbContext.FacultyMembers.Remove(facultyMembers);
            applicationDbContext.SaveChanges();

            return RedirectToAction("AdminList");
        }


        //================================Admin Course Related=================================================

        // making a list for the Courses
        [HttpGet]
        public ActionResult AdminCourseList()
        {
     
                var Courses = applicationDbContext.Courses.ToList();

                var viewModel = new CourseListViewModel
                {
                    courses = Courses.Select(a => new CourseListViewModel.CourseItem
                    {
                        course_Id = a.course_Id,
                        course_Title = a.course_Title,
                        course_Code = a.course_Code
                        
                    }).ToList()
                };

                return View(viewModel);
        }

        //Create a new user(faculty member) to the system from adminstration page

        [HttpGet]
        public ActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCoursePost(Course viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var course = new Course
            {
                course_Id = viewModel.course_Id,
                course_Title = viewModel.course_Title,
                course_Code = viewModel.course_Code
            };

            applicationDbContext.Courses.Add(course);
            applicationDbContext.SaveChanges();

            return RedirectToAction("AdminCourseList");
        }
        //------------Edit----------------------
        [HttpGet]
        public IActionResult EditCourse(int id)
        {
            var Course = applicationDbContext.Courses
                .FirstOrDefault(a => a.course_Id == id);

            var viewModel = new CourseEditViewModel
            {
                Course_Id = Course.course_Id,
                Course_Title = Course.course_Title,
                Course_Code = Course.course_Code
            };

            return View(viewModel);
        }
        //-----------------------------------

        [HttpPost]
        public IActionResult EditCoursePost(CourseEditViewModel viewModel)
        {


            var Course = new Course
            {
                course_Id = viewModel.Course_Id,
                course_Title = viewModel.Course_Title,
                course_Code = viewModel.Course_Code
            };

            applicationDbContext.Courses.Update(Course);
            applicationDbContext.SaveChanges();

            return RedirectToAction("AdminCourseList");
        }

        //=================================================================================

        [HttpGet]
        public IActionResult DeleteCourse(int id)
        {
            var DelCourse = applicationDbContext.Courses
                .FirstOrDefault(a => a.course_Id == id);

            var viewModel = new CourseDeleteViewModel
            {
                Course_Id = DelCourse.course_Id,
                Course_Title = DelCourse.course_Title,
                Course_Code = DelCourse.course_Code
                
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DeleteCoursePost(int id)
        {
            var Deletedcourse = applicationDbContext.Courses
                .FirstOrDefault(a => a.course_Id == id);
            

            applicationDbContext.Courses.Remove(Deletedcourse);
            applicationDbContext.SaveChanges();

            return RedirectToAction("AdminCourseList");
        }

    }
}
