using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SeniorProject.Data;
using SeniorProject.Models;

namespace SeniorProject.Controllers
{
    public class LoginController : Controller
    {

        private readonly ApplicationDbContext applicationDbContext;

        public LoginController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        // GET: LoginController
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Instruct()
        {
            return View();
        }
        
        

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Auth(FacultyMembers member)
        {
            // checking if info match db
            var currentMember = applicationDbContext.FacultyMembers.FirstOrDefault(
                x => x.AcademicID == member.AcademicID && x.Password == member.Password);

            //checking role
            //var roleCheck = applicationDbContext.FacultyMembers.FirstOrDefault(x => x.Role == "admin");


            if (currentMember != null)
            {
                if (currentMember.Role == "admin")
                {
                    
                    return View();

                }
                else
                {
                    ViewData["thisdata"] = currentMember;
                    return View("/Views/Home/Index.cshtml");
                }
            }else
            { TempData["Message"] = "Wrong Password or id"; }
            return RedirectToAction("Index");
        }
    }
}
