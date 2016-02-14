using DataServices;
using DataServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELearning.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }
        #region Courses
        [HttpGet]
        public ActionResult CourseEdit(int CourseId)
        {
            CourseServices services = new CourseServices();
            CourseModel model = new CourseModel();
            model = services.GetCourse(CourseId);
            return View(model);
        }

        [HttpPost]
        public ActionResult CourseEdit(CourseModel model)
        {
            CourseServices services = new CourseServices();
            services.CourseEdit(model);
            return RedirectToAction("Index", "Teacher");
        }

        [HttpGet]
        public ActionResult CourseDelete(int CourseId)
        {
            CourseServices services = new CourseServices();
            CourseModel model = new CourseModel();
            model = services.GetCourse(CourseId);
            return View(model);
        }

        [HttpPost]
        public ActionResult CourseDelete(CourseModel model)
        {
            CourseServices services = new CourseServices();
            model.Tasks = services.GetTasks(model.CourseId);
            services.CourseDelete(model);
            return RedirectToAction("Index", "Teacher");
        }
        #endregion
        #region Tasks
        [HttpGet]
        public ActionResult ManageTasks(int CourseId)
        {
            CourseServices service = new CourseServices();
            CourseModel model = new CourseModel();
            model = service.GetCourse(CourseId);
            model.Tasks = service.GetTasks(CourseId);
            return View(model.Tasks);
        }
        [HttpGet]
        public ActionResult CreateNewTask(int CourseId)
        {
            CourseServices service = new CourseServices();
            TaskModel model = new TaskModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateNewTask(TaskModel model)
        {
            CourseServices service = new CourseServices();
            service.NewTask(model);
            return RedirectToAction("ManageTasks");
        }

        [HttpGet]
        public ActionResult TaskEdit(int TaskId)
        {
            CourseServices service = new CourseServices();
            TaskModel model = new TaskModel();
            model = service.GetTask(TaskId);
            return View(model);
        }
        [HttpPost]
        public ActionResult TaskEdit(TaskModel model)
        {
            CourseServices service = new CourseServices();
            service.EditTask(model);
            return RedirectToAction("ManageTasks", model.CourseId);
        }

        [HttpGet]
        public ActionResult TaskSolutions(int TaskId)
        {
            TaskModel task = new TaskModel();
            CourseServices service = new CourseServices();
            task = service.GetTask(TaskId);
            List<SolutionModel> solutions = new List<SolutionModel>();
            solutions = service.GetSolutions(TaskId);
            return View(solutions);
        }
        #endregion


        [HttpGet]
        public ActionResult NoteEdit(int SolutionId)
        {
            SolutionModel solution = new SolutionModel();
            CourseServices service = new CourseServices();
            solution = service.GetSolution(SolutionId);
            return View(solution);
        }
        [HttpPost]
        public ActionResult NoteEdit(SolutionModel model)
        {
            CourseServices service = new CourseServices();
            service.EditNote(model);
            return RedirectToAction("TaskSolution",model.TaskId);
        }

        [HttpGet]
        public ActionResult StudentSolution(int SolutionId)
        {
            SolutionModel model = new SolutionModel();
            CourseServices service = new CourseServices();
            model = service.GetSolution(SolutionId);
            return View(model);
        }

        [HttpGet]
        public FileContentResult Download(int SolutionId)
        {
            CourseServices cs = new CourseServices();
            SolutionModel model = new SolutionModel();
            model = cs.GetSolution(SolutionId);
            FileServices fs = new FileServices();
            string ContentType = fs.GetContentType(model.Extension);
            return File(model.Solution,ContentType,model.FileName);
        }

    }
}