using Microsoft.AspNetCore.Mvc;
using SeniorProject.Data;
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

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ArticulationMatrixList()
        {
            var Arti = applicationDbContext.ArticulationMatrix.ToList();

            var viewModel = new ArticulationMatrixListViewModel
            {
                ArticulationMatrix = Arti.Select(a => new ArticulationMatrixListViewModel.ArticulationMatrixItim
                {
                    Id = a.Id,
                    teacherCourse_Ref = a.teacherCourse_Ref,
                    CLO = a.CLO,
                    LOD_Ref = a.LOD_Ref,
                    SO = a.SO,
                    Assessing_SO = a.Assessing_SO
                }).ToList()
            };

            return View(viewModel);
        }
    }
}
