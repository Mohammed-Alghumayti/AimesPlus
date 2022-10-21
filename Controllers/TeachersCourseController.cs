using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SeniorProject.Data;
using SeniorProject.Models;
using SeniorProject.ViewModels;
using System.Linq;

namespace SeniorProject.Controllers
{
    public class TeachersCourseController : Controller
    {
        //database objext to use throughout this controller
        private readonly ApplicationDbContext applicationDbContext;

        public TeachersCourseController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        // GET: TeacherCourseController
        [HttpGet]
        public IActionResult List()
        {
            // get TeacherCourse from database
            var teachersCourses = applicationDbContext.TeachersCourse.Include(t => t.teacher_Ref).Include(c => c.course_Ref).ToList();

            // convert model to view model
            var viewModel = new TeachersCourseListViewModel
            {
                teachersCourses = teachersCourses.Select(t => new TeachersCourseListViewModel.TeachersCoursesItem
                {
                    
                    teacher_Ref =t.teacher_Ref,
                    course_Ref =t.course_Ref,
                    SemesterStart =t.SemesterStart,
                    SemesterEnd =t.SemesterEnd,
                }).ToList()
            };

            // pass the view model to the view
            return View(viewModel);
        }


        // GET: TeacherCourseController/Create
        [HttpGet]
        public ActionResult Create()
        {

            //read all teachers from db
            var teachers = applicationDbContext.Teachers.ToList();

            //read all Courses from db
            var courses = applicationDbContext.Courses.ToList();

            //create a viewmodel and fill the select option for teacher and courses for better user exp
            var viewModel = new TeachersCourseCreateViewModel
            {
                //fill the teacher select list
                TeachersList = teachers.Select(t => new SelectListItem()
                {
                    Text = t.teacher_Name,
                    Value = t.teacher_Id.ToString()
                }).ToList(),

                //fill course select list
                CoursesList = courses.Select(c => new SelectListItem()
                {
                    Text = c.course_Code,
                    Value = c.course_Id.ToString()
                }).ToList()

            };

            //pass viewmodel to the view
            return View(viewModel);
        }

        // POST: TeacherCourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(TeachersCourseCreateViewModel viewModel, IFormCollection collection)
        {

            //collect teacher and course sellection from the form
            var collectTeacher = Convert.ToInt64(collection["teacher_Ref"]);
            var collectCourse = Convert.ToInt64(collection["course_Ref"]);

            //get the user selected teacher from db
            var teacher = applicationDbContext.Teachers
                .FirstOrDefault(t => t.teacher_Id == collectTeacher);

            //get the user selected Course from db
            var course = applicationDbContext.Courses
                .FirstOrDefault(c => c.course_Id == collectCourse);

            //convert the whole view model into a model
            var teachersCourse = new TeachersCourse
            {
                teacherCourse_Id = viewModel.teacherCourse_Id,
                teacher_Ref = teacher,
                course_Ref = course,
                SemesterStart = viewModel.SemesterStart,
                SemesterEnd = viewModel.SemesterEnd
            };

            //save the model into the database
            applicationDbContext.TeachersCourse.Add(teachersCourse);
            applicationDbContext.SaveChanges();

            //redirect to the list page
            return RedirectToAction("List");
        }

        // GET: TeacherCourseController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TeacherCourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TeacherCourseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TeacherCourseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
