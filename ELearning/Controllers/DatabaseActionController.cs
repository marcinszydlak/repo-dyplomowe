﻿using DataServices.ViewModels;
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
        #region Login As Administrator
        // GET: DatabaseAction
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(AdminLogin admin)
        {
            if((admin.Login == "Admin") && (admin.Password == "SzkolaOgolnoksztalcaca"))
            {
                Session.Remove("zalogowany");
                Session["admin"] = admin;
                HttpCookie cookie = new HttpCookie("Admin");
                cookie.Values.Add("IsLogged", "1");
                cookie.Expires = DateTime.Now.AddDays(7.0);
                Response.Cookies.Add(cookie);
            }
            else
            {

            }
            return View(admin);
        }
        #endregion
        #region Students Actions
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
            
            return RedirectToAction("Index","DatabaseAction");
        }
        [HttpGet]
        public ActionResult DeleteStudent(int IdUcznia)
        {
            UserServices ServicesForUser = new UserServices();
            StudentModel model = new StudentModel();
            model = ServicesForUser.GetStudent(IdUcznia);
                return View(model);
        }
        [HttpPost]
        public ActionResult DeleteStudent (StudentModel model)
        {
            UserServices ServicesForUser = new UserServices();
            ServicesForUser.DeleteStudent(model);
            return RedirectToAction("ManageStudents");
        }
        [HttpGet]
        public ActionResult EditStudent (int IdUcznia)
        {
            UserServices ServicesForUser = new UserServices();
            StudentModel model = new StudentModel();
            model = ServicesForUser.GetStudent(IdUcznia);
            model.Oddziały = ServicesForUser.GetClasses();
            return View(model);
        }

        [HttpPost]
        public ActionResult EditStudent (StudentModel model)
        {
            UserServices ServicesForUser = new UserServices();
            ServicesForUser.UpdateStudent(model);
            return RedirectToAction("Index");
        }
        #endregion
        #region Teachers Actions

        public ActionResult ManageTeachers()
        {
            UserServices service = new UserServices();
            List<TeacherModel> teachers = new List<TeacherModel>();
            teachers = service.GetTeachers();
            return View(teachers);
        }

        [HttpGet]
        public ActionResult AddNewTeacher()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewTeacher(TeacherModel model)
        {
            UserServices service = new UserServices();
            service.CreateNewTeacher(model.Login,model.Hasło,model.Imię,model.Nazwisko);
            return RedirectToAction("ManageTeachers","DatabaseAction");
        }

        [HttpGet]
        public ActionResult EditTeacher(int IdNauczyciela)
        {
            UserServices service = new UserServices();
            TeacherModel model = new TeacherModel();
            model = service.GetTeacher(IdNauczyciela);
            return View(model);
        }
        #endregion
        [HttpGet]
        public ActionResult Logout()
        {
                Request.Cookies.Remove("Admin");
                Session.Remove("admin");
                return RedirectToAction("Main", "MainPage");
        }
    }
}