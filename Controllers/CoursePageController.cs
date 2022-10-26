using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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


        public IActionResult PDF()
        {
            return View();
        }

        #region List
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


            //this is to add the semester to the coverpage later
            var tcObj = applicationDbContext.TeachersCourse.FirstOrDefault(a => a.course_Ref.course_Code == coursecode);
            HttpContext.Session.SetString(Helper.Semester, tcObj.SemesterStart);

            //return to view
            HttpContext.Session.SetString(Helper.Semester, tcObj.SemesterStart);

            HttpContext.Session.SetString(Helper.course_CODE, tcObj.course_Ref.course_Code);

            HttpContext.Session.SetString(Helper.course_NAME, tcObj.course_Ref.course_Title);


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
            ViewBag.IdCourse = coursecode;
            return View(viewModel);
        }

        #endregion

        #region Add Topic
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

            return RedirectToAction("List", new
            {
                coursecode = course.course_Code
            });

        }

        #endregion

        #region Delete
        // GET: Course/Delete/5
        [HttpGet]
        public ActionResult DeleteTopic(int id)
        {
            return View();
        }

        // POST: Course/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTopicPost(IFormCollection collection,string idCourse)
        {
            try
            {
                var id = Convert.ToInt64(collection["Item.Id"]);

                //get the selected Course topic from db
                var Ctopic = applicationDbContext.CourseCatalog.FirstOrDefault(a => a.Id == id);


                // delete the selected CourseCatalog
                applicationDbContext.CourseCatalog.Remove(Ctopic);
                applicationDbContext.SaveChanges();
                return RedirectToAction("List",new
                {
                    coursecode = idCourse
                });
            }
            catch (Exception)
            {
                return RedirectToAction("List");
                
            }
            

        }

        #endregion
    }
}
