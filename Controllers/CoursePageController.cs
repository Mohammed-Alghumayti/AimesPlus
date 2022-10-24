using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeniorProject.Data;
using SeniorProject.Models;
using SeniorProject.ViewModels;

namespace SeniorProject.Controllers
{
    public class CoursePageController : Controller
    {

        //database object to use throughout this controller
        private readonly ApplicationDbContext applicationDbContext;
        public CoursePageController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }


        // GET: Course
        [HttpGet]
        public ActionResult List(string coursecode)
        {
            //get course obj from database
            var course = applicationDbContext.Courses.FirstOrDefault(x => x.course_Code == coursecode);

            //get course catalog using course id
            var catalogObj = applicationDbContext.CourseCatalog.Where(y => y.course_Ref == course).ToList();

            var artic = applicationDbContext.ArticulationMatrix.Include(t => t.course_Ref)
                .Include(t => t.AssessmentTools)
                .ThenInclude(t => t.AssessmentTools_Ref)
                .Include(t => t.Activities)
                .ThenInclude(t => t.activity_Ref)
                .Include(t => t.LOD_Ref).Where(y => y.course_Ref == course).ToList();

            

            //return to view

            var viewModel = new CoursePageListViewModel
            {
                Course = new Course
                {
                    course_Code = coursecode,
                    course_Id = course.course_Id,
                    course_Title = course.course_Title
                },

                catalog = catalogObj.Select(a => new CoursePageListViewModel.CourseCatalogItem
                {
                    WeekNo = a.WeekNo,
                    catalog_topic = a.catalog_topic,
                    course_Ref = a.course_Ref,
                    details = a.details,
                    Id = a.Id
                }).ToList(),

                Articulation = artic.Select(a => new CoursePageListViewModel.ArticulationMatrixItem
                {
                    Id = a.Id,
                    course_Ref = a.course_Ref,
                    CLO = a.CLO,
                    LOD_Ref = a.LOD_Ref,
                    SO = a.SO,
                    Assessing_SO = a.Assessing_SO,
                    AssessmentTools = a.AssessmentTools,
                    Activities = a.Activities

                }).ToList()

            };

            ViewData["CourseName"] = coursecode;
            return View(viewModel);
        }



        // GET: Course/Create
        [HttpGet]
        public ActionResult AddTopic(string Course)
        {
            var course = applicationDbContext.Courses.FirstOrDefault(a => a.course_Code == Course);
            ViewData["CourseObj"] = course;
            return View("AddTopic");
        }

        // POST: Course/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTopicPost(CourseCatalog viewModel, IFormCollection collection)
        {

            var id = Convert.ToInt64(collection["courseID"]);
            var course = applicationDbContext.Courses.FirstOrDefault(a => a.course_Id == id);

            

            var CourseTopic = new CourseCatalog
            {
                
                course_Ref = course,
                WeekNo = viewModel.WeekNo,
                catalog_topic = viewModel.catalog_topic,
                details = viewModel.details

            };

            applicationDbContext.CourseCatalog.Add(CourseTopic);
            applicationDbContext.SaveChanges();

                return RedirectToAction("List");
            
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Course/Edit/5
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

        // GET: Course/Delete/5
        [HttpGet]
        public ActionResult DeleteTopic(int id)
        {
            return View();
        }

        // POST: Course/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTopicPost(IFormCollection collection)
        {
            try
            {
                var id = Convert.ToInt64(collection["Item.Id"]);

                //get the selected Course topic from db
                var Ctopic = applicationDbContext.CourseCatalog.FirstOrDefault(a => a.Id == id);


                // delete the selected CourseCatalog
                applicationDbContext.CourseCatalog.Remove(Ctopic);
                applicationDbContext.SaveChanges();
                return RedirectToAction("List");
            }
            catch (Exception)
            {
                return RedirectToAction("List");
                
            }
            

        }
    }
}
