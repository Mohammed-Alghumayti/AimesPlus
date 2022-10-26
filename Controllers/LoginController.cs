using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using SeniorProject.Data;
using SeniorProject.Models;
using SeniorProject.ViewModels;
using System.Linq;
using System.Xml.Linq;

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


        //----------------------------------
        #region CheckLogin+Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        // information from login page is passed to parameter from login index view
        public ActionResult check(FacultyMembers member)
        {
            // checking if user info is available in the database
                var currentMember = applicationDbContext.FacultyMembers.FirstOrDefault(
                x => x.AcademicID == member.AcademicID && x.Password == member.Password);

            //checking user role
            //1- If user is Admin- Go to admin page
            //2- If user is Faculty- Go to users Home Page

            if (currentMember != null)
            {

                if (currentMember.Role == "Admin")
                {
                    return View("AdminHome");
                }
                else
                {
                    //passing information to the session helper to maintain while session is Active
                    TempData["name"] = currentMember.Name;
                    HttpContext.Session.SetString(Helper.ROLE, currentMember.Role);
                    HttpContext.Session.SetString(Helper.UserName, currentMember.Name);
                    HttpContext.Session.SetInt32(Helper.UserId, currentMember.Id);

                    return RedirectToAction("Index", "Home", currentMember);
                }
            }
            else
            { TempData["Message"] = "Wrong Password or id";  }
            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            if (CheckAuth(HttpContext))
            {
                HttpContext.Session.Clear();
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region FacultyMembers List
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
        #endregion


        #region Create Faculty
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
            try
            {
                var member = new FacultyMembers
                {
                    AcademicID = viewModel.AcademicID,
                    Name = viewModel.Name,
                    Password = viewModel.Password,
                    Role = viewModel.Role,

                };
                if(viewModel.Role.Equals("Instructor")|| viewModel.Role.Equals("Coordinator")) { 
                var teacher = new Teachers
                {               
                    teacher_Name = viewModel.Name,
                    AcademicId = viewModel.AcademicID
                };
                applicationDbContext.Teachers.Add(teacher);
                    }

                applicationDbContext.FacultyMembers.Add(member);
            
                applicationDbContext.SaveChanges();

                return RedirectToAction("AdminList");
            }
            catch (Exception ex)
            {

                { TempData["Message1"] = "Wrong information or maybe Empty!"; }
                return RedirectToAction("Create");

            }
            
        }
        #endregion
        //=================================================================================

        #region Edit Faculty
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

        #endregion


        #region Delete Faculty
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


        #endregion


        //================================Admin Course Related=================================================

        #region CourseList
        //making a list for the Courses
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

        #endregion


        //Create a new user(faculty member) to the system from adminstration page

        #region Add new Course
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

        #endregion
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
        public IActionResult DeleteCoursePost(int id, IFormCollection collection)
        {
           
         var x = Convert.ToInt64(collection["x"]);

            var course = applicationDbContext.Courses
                .FirstOrDefault(a => a.course_Id == x);
           
            applicationDbContext.Courses.Remove(course);
            applicationDbContext.SaveChanges();

            return RedirectToAction("AdminCourseList");
        }

        public static bool CheckAuth(HttpContext context)
        {
            return !String.IsNullOrEmpty(context.Session.GetString(Helper.UserName));
        }


        [HttpGet]
        public IActionResult AdminHome()
        {
            return View();
        }
    }
}