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

            

            return View();
        }
    }
}