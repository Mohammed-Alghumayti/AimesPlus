using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SeniorProject.Data;
using SeniorProject.Models;
using SeniorProject.ViewModels;
using SeniorProject.ViewModels.ArticulationMatrix;

namespace SeniorProject.Controllers
{
    public class ArticulationMatrixController : Controller
    {

        private readonly ApplicationDbContext applicationDbContext;

        public ArticulationMatrixController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        

        [HttpGet]
        public IActionResult Create(string? Course ,int? idArticulationMatrix)
        {
            var articulationMatrix = new ArticulationMatrix();
            var lods = applicationDbContext.LOD.AsNoTracking()
                .Select(e => new SelectListItem
                {
                    Value = e.LOD_Id.ToString(),
                    Text = e.LOD_Name,
                }).ToList();

            var activitiesList = applicationDbContext.Activities.AsNoTracking()
              .Select(e => new SelectListItem
              {
                  Value = e.Activities_Id.ToString(),
                  Text = e.Name,
              }).ToList();

            var assessmentTools = applicationDbContext.AssessmentTools.AsNoTracking()
            .Select(e => new SelectListItem
            {
                Value = e.toolId.ToString(),
                Text = e.name,
            }).ToList();

            //Create page
            if (Course != null)
            {
                var course = applicationDbContext
               .Courses
               .Where(a => a.course_Code == Course)
               .FirstOrDefault();

                articulationMatrix.courseId = course.course_Id!;
            }
            else
            {
                //EditPage 
                if(idArticulationMatrix != null)
                {
                    articulationMatrix = applicationDbContext
                             .ArticulationMatrix
                             .Where(e => e.Id == idArticulationMatrix)
                             .Include(e => e.AssessmentTools).ThenInclude(e=>e.AssessmentTools_Ref)
                             .Include(e=>e.course_Ref)
                             .Include(e => e.Activities).ThenInclude(e=>e.activity_Ref)
                             .FirstOrDefault();
                    articulationMatrix.courseId = articulationMatrix.course_Ref.course_Id!;
                }
            }


            ViewBag.lods = lods;
            ViewBag.assessmentTools = assessmentTools;
            ViewBag.acteveties = activitiesList;
            return View(articulationMatrix);
        }

        [HttpPost]
        [HttpPost]
        public IActionResult CreateCLO(ArticulationMatrix model)
        {
            try
            {
                if (model == null)
                {
                    return RedirectToAction("List");
                }

                applicationDbContext.ArticulationMatrix.Add(model);
                applicationDbContext.SaveChanges();
                var articulationMatrix = applicationDbContext.ArticulationMatrix
                    .Where(a => a.courseId == model.courseId)
                    .FirstOrDefault().Id;

                return RedirectToAction("Create", new
                {
                    idArticulationMatrix = articulationMatrix,
                });

            }
            catch (Exception ex)
            {
                return RedirectToAction("List");
            }

        }
        [HttpPost]
        public IActionResult addAssessmentTools(int idAssessmentTool, int week,int idArticulationMatrix)
        {
            try
            {

                var articulationMatrix = applicationDbContext.ArticulationMatrix.Where(a => a.Id == idArticulationMatrix).FirstOrDefault();
                var assessmentTool = applicationDbContext.AssessmentTools.Where(a => a.toolId == idAssessmentTool).FirstOrDefault();


                var relation = new ArticulationMatrixAssessmentTools();
                relation.WeekNo = week;
                relation.AssessmentTools_Ref = assessmentTool;
                relation.ArticulationMatrix_Ref = articulationMatrix;

                applicationDbContext.ArticulationMatrixAssessmentTools.Add(relation);
                applicationDbContext.SaveChanges();

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
        public IActionResult addActivities(int idArticulationMatrix, List<int> idActivities)
        {
            try
            {

                var articulationMatrix = applicationDbContext.ArticulationMatrix
                    .Include(e=>e.Activities)
                    .Where(a => a.Id == idArticulationMatrix).FirstOrDefault();
                articulationMatrix.Activities.Clear();
                foreach (var id in idActivities)
                {
                    var relation = new ArticulationMatrixActivities();
                    var activityRef = applicationDbContext.Activities.Where(a => a.Activities_Id == id).FirstOrDefault();

                    relation.ArticulationMatrix_Ref = articulationMatrix;
                    relation.activity_Ref = activityRef;

                    applicationDbContext.ArticulationMatrixActivities.Add(relation);
                }

              
                applicationDbContext.SaveChanges();

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
        public IActionResult CreatePost(ArticulationMatrixCreateViewModel model)
        {

            var data = "";
            return RedirectToAction("List");
        }
    }
}