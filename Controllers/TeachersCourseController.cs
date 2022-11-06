﻿using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
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
        private readonly INotyfService _toastNotification;

        public TeachersCourseController(ApplicationDbContext applicationDbContext , INotyfService _toastNotification)
        {
            this.applicationDbContext = applicationDbContext;
            this._toastNotification = _toastNotification;
        }

        #region List
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
                    teacherCourse_Id =t.teacherCourse_Id,
                     teacher_Ref =t.teacher_Ref,
                     course_Ref =t.course_Ref,
                    SemesterStart =t.SemesterStart,
                    SemesterEnd =t.SemesterEnd,
                }).ToList()
            };

            // pass the view model to the view
            return View(viewModel);
        }
        #endregion

        #region Create
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

            
            var tc = applicationDbContext.TeachersCourse.
                Include(t => t.teacher_Ref).Include(t => t.course_Ref).
                FirstOrDefault(t => t.teacher_Ref == teacher && t.course_Ref == course);


            if (tc == null)
            {

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

                _toastNotification.Success("Succeed Assign Course to Faculty Member");

                //redirect to the list page
                return RedirectToAction("List");
            }
            else
            {
                _toastNotification.Error("This Instructor have this Course alredy");

                return RedirectToAction("Create", "TeachersCourse");
            }
        }
        #endregion

        #region Delete
        // GET: TeacherCourseController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            //find selected TeacherCourse from db
            var tc = applicationDbContext.TeachersCourse
                .Include(t => t.teacher_Ref)
                .Include(t => t.course_Ref)
                .FirstOrDefault(t => t.teacherCourse_Id == id);

            // Convert model to the view model so the user can see selected choice       
            var viewModel = new TeachersCourseDeleteViewModel
            {
                teacherCourse_Id = tc.teacherCourse_Id,
                course_Ref = tc.course_Ref,
                teacher_Ref = tc.teacher_Ref,
                SemesterStart = tc.SemesterStart,
                SemesterEnd = tc.SemesterEnd
            };

            return View(viewModel);
        }

        // POST: TeacherCourseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(TeachersCourseDeleteViewModel t)
        {
            //get the selected TeacherCourse from db
            var tc = applicationDbContext.TeachersCourse.Find(t.teacherCourse_Id);
                

            //delete the selected TeacherCourse
            applicationDbContext.TeachersCourse.Remove(tc);
            applicationDbContext.SaveChanges();

            _toastNotification.Success("Succeed Delete Course");

            return RedirectToAction("List");
        }
        #endregion




        [HttpGet]
        public IActionResult Search(string query)
        {
            var TeachersList = applicationDbContext.TeachersCourse
                .Include(t => t.teacher_Ref)
                .Include(c => c.course_Ref)
                .Where(a => a.teacher_Ref.teacher_Name.Contains(query) ||
                            a.teacher_Ref.AcademicId.ToString().Contains(query) ||
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

            return View("List", viewModel);


        }

    }
}
