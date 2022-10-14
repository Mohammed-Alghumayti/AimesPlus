using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SeniorProject.Data;
using SeniorProject.Models;
using SeniorProject.ViewModels;

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
        public ActionResult check(FacultyMembers member)
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

                    return RedirectToAction("AdminList");

                }
                else
                {
                    ViewData["thisdata"] = currentMember;
                    return View("/Views/Home/Index.cshtml");
                }
            }
            else
            { TempData["Message"] = "Wrong Password or id"; }
            return RedirectToAction("Index");
        }

        // making a list for the adminstrator

        [HttpGet]
        public IActionResult AdminList()
        {
            var FacultyMembers = applicationDbContext.FacultyMembers.ToList();

            var viewModel = new FacultyMembersListViewModel
            {
                FacultyMembers = FacultyMembers.Select(a => new FacultyMembersListViewModel.FacultyMembersItem
                {
                    AcademicID = a.AcademicID,
                    Name = a.Name,
                    Role = a.Role
                }).ToList()
            };

            return View(viewModel);
        }



        //Create a new user(faculty member) to the system from adminstration page
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(FacultyMembers viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var member = new FacultyMembers
            {
                AcademicID = viewModel.AcademicID,
                Name = viewModel.Name,
                Password = viewModel.Password,
                Role = viewModel.Role,
                
            };

            applicationDbContext.FacultyMembers.Add(member);
            applicationDbContext.SaveChanges();

            return RedirectToAction("AdminList");
        }
    }
}
