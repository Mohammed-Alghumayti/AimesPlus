using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        private readonly INotyfService _toastNotification;

        public CoursePageController(ApplicationDbContext applicationDbContext, INotyfService _toastNotification)
        {
            this.applicationDbContext = applicationDbContext;
            this._toastNotification = _toastNotification;
        }




        public IActionResult PDF(string coursecode, string exam)
        {
            var articulation = applicationDbContext.ArticulationMatrix.Include(t => t.course_Ref)
                .Include(t => t.AssessmentTools)
                .ThenInclude(t => t.AssessmentTools_Ref)
                .Include(t => t.Activities)
                .ThenInclude(t => t.activity_Ref)
                .Include(t => t.LOD_Ref)
                .Where(a => a.course_Ref.course_Code == coursecode).ToList();

            var grade = applicationDbContext.GradeDistribution
                .Where(a => a.coursecode == articulation[0].course_Ref.course_Code).ToList();

            // fill viewmodel to send to the view
            var viewModel = new CoursePagePDFViewModel
            {
                exam = exam,
                Articulation = articulation.Select(a => new CoursePagePDFViewModel.ArticulationMatrixItem
                {
                    Id = a.Id,
                    course_Ref = a.course_Ref,
                    CLO = a.CLO,
                    LOD_Ref = a.LOD_Ref,
                    SO = a.SO,
                    Assessing_SO = a.Assessing_SO,
                    AssessmentTools = a.AssessmentTools,
                    Activities = a.Activities,
                    articNum = a.articNum

                }).ToList(),
                GradeDist = grade.Select(a => new CoursePagePDFViewModel.GradeDistributionItem
                {
                    Id = a.Id,
                    coursecode = a.coursecode,
                    Assessment = a.Assessment,
                    assessing_SO = a.assessing_SO,
                    week_Number = a.week_Number,
                    SOchoice = a.SOchoice,
                    SO1 = a.SO1,
                    SO2 = a.SO2,
                    SO3 = a.SO3,
                    SO4 = a.SO4,
                    SO5 = a.SO5,
                    SO6 = a.SO6,
                    percentage = a.percentage

                }).ToList()
            };

            return View(viewModel);
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

            //get an Articulation matrix object based on the course
            var artic = applicationDbContext.ArticulationMatrix.Include(t => t.course_Ref)
                .Include(t => t.AssessmentTools)
                .ThenInclude(t => t.AssessmentTools_Ref)
                .Include(t => t.Activities)
                .ThenInclude(t => t.activity_Ref)
                .Include(t => t.LOD_Ref).Where(y => y.course_Ref == course).ToList();


            //this is to add the semester to the coverpage later
            var tcObj = applicationDbContext.TeachersCourse.FirstOrDefault(a => a.course_Ref.course_Code == coursecode);


            //return to view
            HttpContext.Session.SetString(Helper.Semester, tcObj.SemesterStart);
            HttpContext.Session.SetString(Helper.course_CODE, tcObj.course_Ref.course_Code);
            HttpContext.Session.SetString(Helper.course_NAME, tcObj.course_Ref.course_Title);


            // Preparing GRADE DISTRIBUTION
            UpdateGradeDistribution(coursecode);

            var GradeDist = applicationDbContext.GradeDistribution.Where(a => a.coursecode == coursecode).ToList();

            // fill viewmodel to send to the view
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
                    Activities = a.Activities,
                    articNum = a.articNum

                }).ToList(),


                GradeDist = GradeDist.Select(a => new CoursePageListViewModel.GradeDistributionItem
                {
                    Id = a.Id,
                    coursecode = a.coursecode,
                    Assessment = a.Assessment,
                    assessing_SO = a.assessing_SO,
                    week_Number = a.week_Number,
                    SOchoice = a.SOchoice,
                    SO1 = a.SO1,
                    SO2 = a.SO2,
                    SO3 = a.SO3,
                    SO4 = a.SO4,
                    SO5 = a.SO5,
                    SO6 = a.SO6,
                    percentage = a.percentage

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

            _toastNotification.Success("Success adding topic");

            return RedirectToAction("List", new
            {
                coursecode = course.course_Code
            });

        }

        #endregion



        #region Delete

        // POST: Course/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTopicPost(IFormCollection collection, string idCourse)
        {
            try
            {
                var id = Convert.ToInt64(collection["Item.Id"]);

                //get the selected Course topic from db
                var Ctopic = applicationDbContext.CourseCatalog.FirstOrDefault(a => a.Id == id);


                // delete the selected CourseCatalog
                applicationDbContext.CourseCatalog.Remove(Ctopic);
                applicationDbContext.SaveChanges();

                _toastNotification.Success("Success Deleteing topic");

                return RedirectToAction("List", new
                {
                    coursecode = idCourse
                });
            }
            catch (ArgumentNullException ex)
            {
                _toastNotification.Error("Somthing went wrong" + ex.Message);
                return RedirectToAction("List");

            }
        }

        #endregion



        [HttpGet]
        public ActionResult PrepareGradeD(string coursecode)
        {
            try
            {
                var gradeD = applicationDbContext.GradeDistribution.FirstOrDefault(a => a.coursecode == coursecode);

                if (gradeD == null)
                {
                    var artList = applicationDbContext.ArticulationMatrix
                        .Where(a => a.course_Ref.course_Code == coursecode).ToList();

                    //gradeD = new GradeDistribution
                    //{
                    //    ArticulationMatrixL = artList,
                    //    coursecode = coursecode

                    //};

                    // Add the selected CourseCatalog
                    applicationDbContext.GradeDistribution.Add(gradeD);
                    applicationDbContext.SaveChanges();

                    ViewData["GradeD"] = gradeD;

                    return View(gradeD);
                }
                else
                {

                    return View(gradeD);

                }

                //return RedirectToAction("List", new
                //{
                //    coursecode = idCourse
                //});

            }
            catch (ArgumentNullException ex)
            {
                _toastNotification.Error("Somthing went wrong" + ex.Message);
                return RedirectToAction("List");

            }

        }


        private bool UpdateGradeDistribution(string courseCode)
        {
            var course = applicationDbContext.Courses.FirstOrDefault(x => x.course_Code == courseCode);
            var gradeDistribution = applicationDbContext.GradeDistribution.FirstOrDefault(a => a.coursecode == courseCode);
            var artic = applicationDbContext.ArticulationMatrix.Include(t => t.course_Ref)
            .Include(t => t.AssessmentTools)
            .ThenInclude(t => t.AssessmentTools_Ref)
            .Include(t => t.Activities)
            .ThenInclude(t => t.activity_Ref)
            .Include(t => t.LOD_Ref)
            .Where(y => y.course_Ref == course)
            .ToList();

            foreach (var item in artic)
            {
                //   If grad distribution not exists Where (Week == item.week && assessment == item.assessment)
                // then insert GD
                // else need to do something with current record?
                foreach (var assess in item.AssessmentTools)
                {
                    var GradeDCheck = applicationDbContext.GradeDistribution
                        .FirstOrDefault(a => a.coursecode == courseCode && a.Assessment == assess.AssessmentTools_Ref.name && a.week_Number == assess.WeekNo);
                    if (GradeDCheck == null)
                    {
                        var GradeItem = new GradeDistribution
                        {
                            coursecode = courseCode,
                            Assessment = assess.AssessmentTools_Ref.name,
                            week_Number = assess.WeekNo,
                            SOchoice = item.SO,
                            assessing_SO = item.Assessing_SO
                        };

                        applicationDbContext.GradeDistribution.Add(GradeItem);
                        applicationDbContext.SaveChanges();

                    }

                }

            }

            return true;
        }


        [HttpGet]
        public IActionResult EditGrades(int id)
        {
            GradeDistribution gradeRecord = applicationDbContext.GradeDistribution.Where(a => a.Id == id).FirstOrDefault();

            return PartialView("_EditGradesPartialView", gradeRecord);

        }


        [HttpPost]
        public IActionResult EditGradesPost(GradeDistribution gradeD)
        {

            var gradeRecord = applicationDbContext.GradeDistribution
                .Where(a => a.coursecode == gradeD.coursecode && a.Assessment == gradeD.Assessment && a.week_Number == gradeD.week_Number).FirstOrDefault();
           
            if (gradeD.percentage == null)
            {
                _toastNotification.Warning("Some fields are empty, make sure to fill it.");
                 RedirectToAction("EditGrades", new { id = gradeRecord.Id });
                
            }
            else
            {
                gradeRecord.percentage = gradeD.percentage;
                gradeRecord.SO1 = gradeD.SO1;
                gradeRecord.SO2 = gradeD.SO2;
                gradeRecord.SO3 = gradeD.SO3;
                gradeRecord.SO4 = gradeD.SO4;
                gradeRecord.SO5 = gradeD.SO5;
                gradeRecord.SO6 = gradeD.SO6;


                applicationDbContext.GradeDistribution.Update(gradeRecord);
                applicationDbContext.SaveChanges();

                _toastNotification.Success("Success Updating Grades");
                
            }
            
           return RedirectToAction("List", new { coursecode = gradeD.coursecode });
        }


    }
}
