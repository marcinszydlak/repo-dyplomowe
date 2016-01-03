using Database;
using DataServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DataServices
{
    public class CourseServices
    {
        KursyELearningDBEntities db = new KursyELearningDBEntities();
        #region Courses
        public void NewCourse(CourseModel model)
        {
            db.Kursy.Add(new Kursy
            {
                IdKursu = db.Kursy.Max(x => x.IdKursu) + 1,
                IdPrzedmiotu = model.SubjectId,
                IdProwadzącego = model.TeacherId,
                IdKlasy = model.ClassId,
                Opis = model.CourseDescription,
                Tytuł = model.CourseTitle,
            });
            db.SaveChanges();
        }
        public List<CourseModel> GetCourses()
        {
            List<CourseModel> kursy = new List<CourseModel>();
            var list = db.Kursy.ToList();
            foreach (var item in list)
            {
                kursy.Add(new CourseModel
                {
                    CourseId = item.IdKursu,
                    ClassId = item.IdKlasy,
                    Class = db.Klasy.Where(x=>x.IdKlasy == item.IdKlasy).Select(x=>x.Oddział).FirstOrDefault(),
                    CourseTitle = item.Tytuł,
                    CourseDescription = item.Tytuł,
                    SubjectId = item.IdPrzedmiotu,
                    Subject = db.Przedmioty.Where(x => x.IdPrzedmiotu == item.IdPrzedmiotu).Select(x => x.NazwaPrzedmiotu).FirstOrDefault(),
                    TeacherId = item.IdProwadzącego,
                    Tasks = GetTasks(item.IdKursu)
                });
            }
            return kursy;
        }
        public List<CourseModel> GetCourses(int TeacherId)
        {
            List<CourseModel> kursy = new List<CourseModel>();
            var list = db.Kursy.Where(x=>x.IdProwadzącego == TeacherId).ToList();
            foreach (var item in list)
            {
                kursy.Add(new CourseModel
                {
                    CourseId = item.IdKursu,
                    ClassId = item.IdKlasy,
                    Class = db.Klasy.Where(x => x.IdKlasy == item.IdKlasy).Select(x => x.Oddział).FirstOrDefault(),
                    CourseTitle = item.Tytuł,
                    CourseDescription = item.Tytuł,
                    SubjectId = item.IdPrzedmiotu,
                    Subject = db.Przedmioty.Where(x => x.IdPrzedmiotu == item.IdPrzedmiotu).Select(x => x.NazwaPrzedmiotu).FirstOrDefault(),
                    TeacherId = item.IdProwadzącego,
                    Tasks = GetTasks(item.IdKursu)
                });
            }
            return kursy;
        }
        #endregion
        #region Tasks
        public void NewTask(TaskModel model)
        {
            db.Zadania.Add(new Zadania
            {
                IdKursu = model.CourseId,
                IdZadania = model.TaskId,
                DataOddania = model.DeadLineDate,
                DataWstawienia = model.CreationDate,
                Treść = model.Descriprion
            });
            db.SaveChanges();
        }
        public TaskModel GetTask(int TaskId)
        {
            var query = db.Zadania.Where(x => x.IdZadania == TaskId).FirstOrDefault();
            TaskModel task = new TaskModel();
            task.CourseId = query.IdKursu;
            task.TaskId = query.IdZadania;
            task.CreationDate = (DateTime)query.DataWstawienia;
            task.DeadLineDate = (DateTime)query.DataOddania;
            task.Descriprion = query.Treść;
            return task;
        }
        public List<TaskModel> GetTasks(int CourseId)
        {
            List<TaskModel> zadania = new List<TaskModel>();
            var list = db.Zadania.Where(x => x.IdKursu == CourseId);
            foreach(var item in list)
            {
                zadania.Add(new TaskModel
                {
                    CourseId = item.IdKursu,
                    CreationDate = (DateTime)item.DataWstawienia,
                    DeadLineDate = (DateTime)item.DataOddania,
                    Descriprion = item.Treść,
                    TaskId = item.IdZadania
                });
            }
            return zadania;
        }
        #endregion
        #region Solutions
        public void newSolution(SolutionModel model)
        {
            db.Rozwiązania.Add(new Rozwiązania()
            {
                IdRozwiązania = model.SolutionId,
                IdUcznia = model.StudentId,
                IdZadania = model.TaskId,
                //TreśćRozwiązania = ,
            });
            
        }
        #endregion
        #region Subjects
        public List<SubjectModel> GetSubjects()
        {
            List<SubjectModel> subjects = new List<SubjectModel>();
            var query = db.Przedmioty.ToList();
            foreach(var item in query)
            {
                subjects.Add(new SubjectModel
                {
                    SubjectId = item.IdPrzedmiotu,
                    SubjectName = item.NazwaPrzedmiotu
                });
            }
            return subjects;
        }
        #endregion
    }

}
