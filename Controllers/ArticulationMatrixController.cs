using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SeniorProject.Data;
using SeniorProject.Models;
using SeniorProject.ViewModels;
using SeniorProject.ViewModels.ArticulationMatrix;
using System.Reflection.Metadata.Ecma335;

namespace SeniorProject.Controllers
{
    public class ArticulationMatrixController : Controller
    {

        private readonly ApplicationDbContext applicationDbContext;
        private readonly INotyfService _toastNotification;

        public ArticulationMatrixController(ApplicationDbContext applicationDbContext , INotyfService _toastNotification)
        {
            this.applicationDbContext = applicationDbContext;
            this._toastNotification = _toastNotification;
        }

        
        //This get the information for the add articulation matrix create
        [HttpGet]
        public IActionResult Create(string? Course ,int? idArticulationMatrix)
        {
            //articulation Matrix Id and course code passed by name from the form


            //Make articulation matrix object to fill data into
            //then Make a list of all the Possible selections
            
            //LOD list
            var articulationMatrix = new ArticulationMatrix();
            var lods = applicationDbContext.LOD.AsNoTracking()
                .Select(e => new SelectListItem
                {
                    Value = e.LOD_Id.ToString(),
                    Text = e.LOD_Name,
                }).ToList();

            //Activity list
            var activitiesList = applicationDbContext.Activities.AsNoTracking()
              .Select(e => new SelectListItem
              {
                  Value = e.Activities_Id.ToString(),
                  Text = e.Name,
              }).ToList();

            //Assesment Tools List
            var assessmentTools = applicationDbContext.AssessmentTools.AsNoTracking()
            .Select(e => new SelectListItem
            {
                Value = e.toolId.ToString(),
                Text = e.name,
            }).ToList();

            //If there's a selected course pass the course object to the Articulation FK
            if (Course != null)
            {
                var course = applicationDbContext
               .Courses
               .Where(a => a.course_Code == Course)
               .FirstOrDefault();

                articulationMatrix.course_Code = course.course_Code!;
            }
            //This means the selected option is to edit the page by the articulation matrix only Object
            else
            {
                //EditPage 
                //Load all related data in the object
                if(idArticulationMatrix != null)
                {
                    articulationMatrix = applicationDbContext
                             .ArticulationMatrix
                             .Where(e => e.Id == idArticulationMatrix)
                             .Include(e => e.AssessmentTools).ThenInclude(e=>e.AssessmentTools_Ref)
                             .Include(e=>e.course_Ref)
                             .Include(e => e.Activities).ThenInclude(e=>e.activity_Ref)
                             .FirstOrDefault();
                    articulationMatrix.course_Code = articulationMatrix.course_Ref.course_Code!;
                }
            }

            //pass all lists to the view

            ViewBag.lods = lods;
            ViewBag.assessmentTools = assessmentTools;
            ViewBag.acteveties = activitiesList;
            ViewData["CourseName"] = Course;

            return View(articulationMatrix);
        }

        

        [HttpPost]
        public IActionResult CreateCLO(ArticulationMatrix model)
        {
            //This is to add a new record of CLO to the database
            try
            {
                if (model == null)
                {
                    return RedirectToAction("List");
                }
                var course = applicationDbContext.Courses.FirstOrDefault(a => a.course_Code == model.course_Code);
                model.course_Ref = course;

                //Add to db
                applicationDbContext.ArticulationMatrix.Add(model);
                applicationDbContext.SaveChanges();

               

                //Pass the value of the id to the view to continue modifications
                var articulationMatrix = applicationDbContext.ArticulationMatrix
                    .Where(a => a.course_Code == model.course_Code) 
                    .FirstOrDefault().Id;

                _toastNotification.Success("Success Adding CLO");

                return RedirectToAction("Create", new
                {
                    idArticulationMatrix = articulationMatrix,
                });

            }
            catch (Exception ex)
            {
                _toastNotification.Error("Somthing went wrong" + ex.Message);
                return RedirectToAction("List");
            }

        }


        [HttpPost]
        public IActionResult addAssessmentTools(int idAssessmentTool, int week,int idArticulationMatrix)
        {
            try
            {

                var articulationMatrix = applicationDbContext.ArticulationMatrix
                    .Where(a => a.Id == idArticulationMatrix).FirstOrDefault();

                var assessmentTool = applicationDbContext.AssessmentTools
                    .Where(a => a.toolId == idAssessmentTool).FirstOrDefault();

                var relation = new ArticulationMatrixAssessmentTools();
                relation.WeekNo = week;
                relation.AssessmentTools_Ref = assessmentTool;
                relation.ArticulationMatrix_Ref = articulationMatrix;

                applicationDbContext.ArticulationMatrixAssessmentTools.Add(relation);
                applicationDbContext.SaveChanges();

                _toastNotification.Success("Success Adding Assessment Tools to CLO");

                return RedirectToAction("Create", new
                {
                    idArticulationMatrix = idArticulationMatrix,
                });

            }
            catch(Exception ex)
            {
                _toastNotification.Error("Somthing went wrong" + ex.Message);
                return RedirectToAction("List");
            }
           
        }  
           [HttpPost]
        public IActionResult addActivities(int idArticulationMatrix, List<int> idActivities)
        {
            try
            {
                //get articulation matrix info to add activites
                var articulationMatrix = applicationDbContext.ArticulationMatrix
                    .Include(e=>e.Activities)
                    .Where(a => a.Id == idArticulationMatrix).FirstOrDefault();
                
                //clear the activity list
                articulationMatrix.Activities.Clear();

                //loop throught the choice of activites chosen and add to db list
                foreach (var id in idActivities)
                {
                    var relation = new ArticulationMatrixActivities();
                    var activityRef = applicationDbContext.Activities.Where(a => a.Activities_Id == id).FirstOrDefault();

                    relation.ArticulationMatrix_Ref = articulationMatrix;
                    relation.activity_Ref = activityRef;

                    applicationDbContext.ArticulationMatrixActivities.Add(relation);
                }          
                //save changes to db and return to the same view
                applicationDbContext.SaveChanges();

                _toastNotification.Success("Success Adding Activities to CLO");

                return RedirectToAction("Create", new
                {
                    idArticulationMatrix = idArticulationMatrix,
                });

            }
            catch(Exception ex)
            {
                return RedirectToAction("List");
            }
           
        }

        [HttpPost]
        public IActionResult DeleteAssessmentTools(int idAssessmentTool, int idArticulationMatrix)
        {
            try
            {

                //GET assessment tool to remove
                var assessmentTool = applicationDbContext.ArticulationMatrixAssessmentTools    
                    .Include(a=> a.AssessmentTools_Ref)
                    .FirstOrDefault(a => a.Id == idAssessmentTool && a.ArticulationMatrix_Ref.Id == idArticulationMatrix);

                //Get Grade Distribution to Remove
                //find articulation to pass the coursecode from
                var articulation = applicationDbContext.ArticulationMatrix  
                    .FirstOrDefault(a => a.Id == idArticulationMatrix );

                var gradeD = applicationDbContext.GradeDistribution
                    .FirstOrDefault(a => a.coursecode == articulation.course_Code && a.Assessment == assessmentTool.AssessmentTools_Ref.name && a.week_Number == assessmentTool.WeekNo);

                //remove assessment tool && remove from grade dist

                applicationDbContext.ArticulationMatrixAssessmentTools.Remove(assessmentTool);
                applicationDbContext.GradeDistribution.Remove(gradeD);
                applicationDbContext.SaveChanges();


                _toastNotification.Success("Success Deleting Assessment Tools from CLO");

                return RedirectToAction("Create", new
                {
                    idArticulationMatrix = idArticulationMatrix,
                });

            }
            catch (Exception ex)
            {
                return RedirectToAction("List");
            }

        }


        [HttpPost]
        public IActionResult DeleteActivity(int idActivity, int idArticulationMatrix)
        {
            try
            {
                var Activity = applicationDbContext.ArticulationMatrixActivities
                    .FirstOrDefault(a => a.Id == idActivity && a.ArticulationMatrix_Ref.Id == idArticulationMatrix);


                applicationDbContext.ArticulationMatrixActivities.Remove(Activity);
                applicationDbContext.SaveChanges();

                _toastNotification.Success("Success Deleting Activities from CLO");

                return RedirectToAction("Create", new
                {
                    idArticulationMatrix = idArticulationMatrix,
                });

            }
            catch (Exception ex)
            {
                return RedirectToAction("List");
            }

        }



        public IActionResult DeleteClo( int idArticulationMatrix)
        {

            var artic = applicationDbContext.ArticulationMatrix
                .Include(a=> a.AssessmentTools).ThenInclude(a=> a.AssessmentTools_Ref)
                .FirstOrDefault(x => x.Id == idArticulationMatrix);

            string coursecode = artic.course_Code;

            //removing assessment tools from the grade distribution
            foreach (var item in artic.AssessmentTools)
            {
                var gradeD = applicationDbContext.GradeDistribution
                    .FirstOrDefault(a => a.coursecode == artic.course_Code && a.Assessment == item.AssessmentTools_Ref.name && a.week_Number == item.WeekNo);
                if (gradeD != null)
                {
                    applicationDbContext.GradeDistribution.Remove(gradeD);
                    applicationDbContext.SaveChanges();

                }


            }

            _toastNotification.Success("Success Deleting CLO");

            applicationDbContext.ArticulationMatrix.Remove(artic);
            applicationDbContext.SaveChanges();
            return RedirectToAction("List", "CoursePage", new
            {
                coursecode = coursecode
            });
        }


    }
}