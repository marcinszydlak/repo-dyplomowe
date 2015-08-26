using DataServices.ViewModels;
using DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELearning.Controllers
{
    public class DatabaseActionController : Controller
    {

        // GET: DatabaseAction
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ManageStudents()
        {
            UserServices service = new UserServices();
            List<StudentModel> students = new List<StudentModel>();
            students = service.GetStudents();
            return View(students);
        }
        [HttpGet]
        public ActionResult AddNewStudent()
        {
            StudentModel model = new StudentModel();
            UserServices ServiceForUser = new UserServices();
            model.Oddziały = ServiceForUser.GetClasses();
            return View(model);

        }
        [HttpPost]
        public ActionResult AddNewStudent(StudentModel model)
        {
            UserServices ServiceForUser = new UserServices();
            ServiceForUser.CreateNewStudent(model);
            
            return RedirectToAction("Index",ViewBag.Message = "Tworzenie wpisu ucznia zakończone powodzeniem");
        }
    }
}