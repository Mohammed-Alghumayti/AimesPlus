using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SeniorProject.Data;
using SeniorProject.Models;
using SeniorProject.ViewModels;
using System.Linq;

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
        
        
        //----------------------------------
        
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
            }else
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
                    Id = a.Id,
                    AcademicID = a.AcademicID,
                    Name = a.Name,
                    Role = a.Role
                }).ToList()
            };

            return View(viewModel);
        }
        //----------------------------------

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var facultyMembers = applicationDbContext.FacultyMembers
                .FirstOrDefault(a => a.Id == id);

            var viewModel = new FacultyMembersEditViewModel
            {
                Id = facultyMembers.Id,
                AcademicID = facultyMembers.AcademicID,
                Role = facultyMembers.Role,
                Password = facultyMembers.Password,
                Name = facultyMembers.Name
            };

            return View(viewModel);
        }
        //-----------------------------------

        [HttpPost]
        public IActionResult EditPost(FacultyMembersEditViewModel viewModel)
        {
            

            var facultyMembers = new FacultyMembers
            {
                Id = viewModel.Id,
                AcademicID = viewModel.AcademicID,
                Role = viewModel.Role,
                Password = viewModel.Password,
                Name = viewModel.Name
            };

            applicationDbContext.FacultyMembers.Update(facultyMembers);
            applicationDbContext.SaveChanges();

            return RedirectToAction("AdminList");
        }
        //---------------------------


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var facultyMembers = applicationDbContext.FacultyMembers
                .FirstOrDefault(a => a.Id == id);

            var viewModel = new FacultyMembersDeleteViewModel
            {
                Id = facultyMembers.Id,
                AcademicID = facultyMembers.AcademicID,
                Role = facultyMembers.Role,
                Password = facultyMembers.Password,
                Name = facultyMembers.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DeletePost(int id)
        {
            var facultyMembers = applicationDbContext.FacultyMembers
                .FirstOrDefault(a => a.Id == id);

            applicationDbContext.FacultyMembers.Remove(facultyMembers);
            applicationDbContext.SaveChanges();

            return RedirectToAction("AdminList");
        }


    }
}
