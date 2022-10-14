using Microsoft.AspNetCore.Mvc;

namespace SeniorProject.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Artic()
        {
            return View("Views/ArticulationMatrix/Index.cshtml");
        }






       

    }
}