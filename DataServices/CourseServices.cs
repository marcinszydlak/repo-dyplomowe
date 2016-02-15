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

        public CourseModel GetCourse(int CourseId)
        {
            var course = db.Kursy.Where(x => x.IdKursu == CourseId).FirstOrDefault();
            CourseModel model = new CourseModel();
            model.CourseId = course.IdKursu;
            model.ClassId = course.IdKlasy;
            model.Class = db.Klasy.Where(x => x.IdKlasy == course.IdKlasy).Select(p => p.Oddział).FirstOrDefault();
            model.CourseDescription = course.Opis;
            model.CourseTitle = course.Tytuł;
            model.SubjectId = course.IdPrzedmiotu;
            model.Subject = db.Przedmioty.Where(x => x.IdPrzedmiotu == course.IdPrzedmiotu).Select(p => p.NazwaPrzedmiotu).FirstOrDefault();
            model.TeacherId = course.IdProwadzącego;
            var tasks = db.Zadania.Where(x => x.IdKursu == CourseId).ToList();
            model.Tasks = new List<TaskModel>();
            foreach(var task in tasks)
            {
                model.Tasks.Add(new TaskModel
                {
                    CourseId = task.IdKursu,
                    CreationDate = (DateTime)task.DataWstawienia,
                    DeadLineDate = (DateTime)task.DataOddania,
                    Descriprion = task.Treść,
                    TaskId = task.IdZadania
                });
            }
            UserServices us = new UserServices();
            model.Subjects = GetSubjects();
            model.Classes = us.GetClasses();
            return model;
        }

        public void CourseEdit(CourseModel model)
        {
            var editedCourse = db.Kursy.Where(x => x.IdKursu == model.CourseId).FirstOrDefault();
            if (editedCourse.Zadania == null)
            {
                editedCourse.IdKlasy = model.ClassId;
                editedCourse.IdPrzedmiotu = model.SubjectId;
                editedCourse.Opis = model.CourseDescription;
                editedCourse.Tytuł = model.CourseTitle;
            }
            else
            {
                var tasks = editedCourse.Zadania;
                foreach(var task in tasks)
                {
                    if(task.Rozwiązania == null)
                    {
                        editedCourse.IdKlasy = model.ClassId;
                        editedCourse.IdPrzedmiotu = model.SubjectId;
                        editedCourse.Opis = model.CourseDescription;
                        editedCourse.Tytuł = model.CourseTitle;
                    }
                    else
                    {
                        db.Rozwiązania.RemoveRange(task.Rozwiązania);
                    }
                }
            }
            db.SaveChanges();
        }

        public void CourseDelete(CourseModel model)
        {

            foreach(var item in model.Tasks)
            {
                var SolutionsToDelete = db.Rozwiązania.Where(x => x.IdZadania == item.TaskId);
                db.Rozwiązania.RemoveRange(SolutionsToDelete);
            }
            var TasksToDelete = db.Zadania.Where(x => x.IdKursu == model.CourseId).ToList();
            db.Zadania.RemoveRange(TasksToDelete);
            var CourseToDelete = db.Kursy.Where(x => x.IdKursu == model.CourseId).FirstOrDefault();
            db.Kursy.Remove(CourseToDelete);
            db.SaveChanges();
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
        public void EditTask(TaskModel model)
        {
            var TaskToEdit = db.Zadania.Where(x => x.IdZadania == model.TaskId).FirstOrDefault();
            TaskToEdit.DataOddania = model.DeadLineDate;
            TaskToEdit.Treść = model.Descriprion;
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
        public void TaskDelete(TaskModel model)
        {
            var query = db.Zadania.Where(x => x.IdZadania == model.TaskId).FirstOrDefault();
            var solutions = db.Rozwiązania.Where(x => x.IdZadania == model.TaskId).ToList();
            db.Rozwiązania.RemoveRange(solutions);
            db.Zadania.Remove(query);
            db.SaveChanges();
        }
        #endregion
        #region Solutions
        public void NewSolution(SolutionModel model)
        {
            if (db.Rozwiązania.Count() > 0)
            {
                db.Rozwiązania.Add(new Rozwiązania()
                {
                    IdRozwiązania = db.Rozwiązania.Max(x => x.IdRozwiązania) + 1,
                    IdUcznia = model.StudentId,
                    IdZadania = model.TaskId,
                    TreśćRozwiązania = model.Solution,
                    DataWstawienia = DateTime.Now,
                    NazwaPliku = model.FileName,
                    Rozszerzenie = model.FileName.Split(new Char[] { '.' }).Last()
                });
            }
            else
            {
                db.Rozwiązania.Add(new Rozwiązania()
                {
                    IdRozwiązania = 1,
                    IdUcznia = model.StudentId,
                    IdZadania = model.TaskId,
                    TreśćRozwiązania = model.Solution,
                    DataWstawienia = DateTime.Now,
                    NazwaPliku = model.FileName,
                    Rozszerzenie = model.FileName.Split(new Char[] { '.' }).Last()
                });
            }
            db.SaveChanges();
            
        }

        public SolutionModel GetSolution(int SolutionId)
        {
            var query = db.Rozwiązania.Where(x => x.IdRozwiązania == SolutionId).FirstOrDefault();
            UserServices us = new UserServices();
            SolutionModel model = new SolutionModel{
                SolutionId = query.IdRozwiązania,
                StudentId = query.IdUcznia,
                TaskId = query.IdZadania,
                Solution = query.TreśćRozwiązania,
                FileName = query.NazwaPliku,
                Extension = query.Rozszerzenie,
                Comment = query.Komentarz,
                Note = query.Ocena,
                Student = us.GetStudent(query.IdUcznia),
                Notes = GetPossibleNotes()
            };
            return model;
        }
        public SolutionModel GetSolution(int TaskId,StudentModel model)
        {
            SolutionModel solution = new SolutionModel();
            var query = db.Rozwiązania.Where(x => x.IdZadania == TaskId && x.IdUcznia == model.IdUcznia).FirstOrDefault();
            solution.Student = model;
            solution.StudentId = model.IdUcznia;
            solution.Note = query.Ocena;
            solution.Notes = GetPossibleNotes();
            solution.SolutionId = query.IdRozwiązania;
            solution.Solution = query.TreśćRozwiązania;
            solution.FileName = query.NazwaPliku;
            solution.Extension = query.Rozszerzenie;
            solution.Comment = query.Komentarz;
            return solution;
        }
        public List<SolutionModel> GetSolutions(int TaskId)
        {
            var query = db.Rozwiązania.Where(x => x.IdZadania == TaskId).ToList();
            List<SolutionModel> solutions = new List<SolutionModel>();
            UserServices us = new UserServices();
            foreach (var item in query)
            {
                var uczen = db.Uczniowie.Where(x => x.IdUcznia == item.IdUcznia).FirstOrDefault();
                solutions.Add(new SolutionModel
                {
                    TaskId = item.IdZadania,
                    StudentId = item.IdUcznia,
                    SolutionId = item.IdRozwiązania,
                    Solution = item.TreśćRozwiązania,
                    FileName = item.NazwaPliku,
                    Extension = item.Rozszerzenie,
                    Comment = item.Komentarz,
                    Note = (byte?)item.Ocena,
                    Student = us.GetStudent(item.IdUcznia)
                });
            }
            return solutions;
        }
        public void EditNote(SolutionModel model)
        {
            var query = db.Rozwiązania.Where(x => x.IdRozwiązania == model.SolutionId).FirstOrDefault();
            query.Komentarz = model.Comment;
            query.Ocena = model.Note;
            db.SaveChanges();
        }
        public void SolutionEdit(SolutionModel model)
        {
            var solutionToEdit = db.Rozwiązania.Where(x => x.IdRozwiązania == model.SolutionId).FirstOrDefault();
            if(!(solutionToEdit.Ocena.HasValue && solutionToEdit.Komentarz != String.Empty))
            {
                solutionToEdit.TreśćRozwiązania = model.Solution;
                solutionToEdit.DataWstawienia = DateTime.Now;
                db.SaveChanges();
            }
        }
        public List<NoteModel> GetPossibleNotes()
        {
            List<NoteModel> notes = new List<NoteModel>();
            notes.Add(new NoteModel { Value = 1, StringValue = "Niedostateczny" });
            notes.Add(new NoteModel { Value = 2, StringValue = "Dopuszczający" });
            notes.Add(new NoteModel { Value = 3, StringValue = "Dostateczny" });
            notes.Add(new NoteModel { Value = 4, StringValue = "Dobry" });
            notes.Add(new NoteModel { Value = 5, StringValue = "Bardzo dobry" });
            notes.Add(new NoteModel { Value = 6, StringValue = "Celujący" });
            return notes;
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
