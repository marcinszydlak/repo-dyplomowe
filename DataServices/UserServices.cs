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
    public class UserServices
    {
        KursyELearningDBEntities db = new KursyELearningDBEntities();

        
        #region Logowanie
        //Pobieranie z bazy danych aktualnie zalogowanego użytkownika
        public UserLoginModel GetLoggedUser(string Login,string Password)
        {
            try
            {
                var studentquery = db.Uczniowie.Where(x => x.Login == Login && x.Hasło == Password).FirstOrDefault();
                if (studentquery != null)
                {
                    UserLoginModel ulm = new UserLoginModel
                    {
                        Login = studentquery.Login,
                        Name = studentquery.Imię,
                        Surname = studentquery.Nazwisko,
                        Password = studentquery.Hasło
                    };
                    return ulm;
                }
                else
                {
                    var teacherquery = db.Nauczyciele.Where(x => x.Login == Login && x.Hasło == Password).FirstOrDefault();
                    if (teacherquery != null)
                    {
                        UserLoginModel ulm = new UserLoginModel
                        {
                            Login = teacherquery.Login,
                            Name = teacherquery.Imię,
                            Surname = teacherquery.Nazwisko,
                            Password = teacherquery.Hasło
                        };
                        return ulm;
                    }
                    return null;
                }
            }
            catch 
            {
                return null;
            }
        }

        public UserLoginModel GetLoggedUser(string Login)
        {
            try
            {
                var studentquery = db.Uczniowie.Where(x => x.Login == Login).FirstOrDefault();
                if (studentquery != null)
                {
                    UserLoginModel ulm = new UserLoginModel
                    {
                        Login = studentquery.Login,
                        Name = studentquery.Imię,
                        Surname = studentquery.Nazwisko

                    };
                    return ulm;
                }
                else
                {
                    var teacherquery = db.Nauczyciele.Where(x => x.Login == Login).FirstOrDefault();
                    if (teacherquery != null)
                    {
                        UserLoginModel ulm = new UserLoginModel
                        {
                            Login = teacherquery.Login,
                            Name = teacherquery.Imię,
                            Surname = teacherquery.Nazwisko
                        };
                        return ulm;
                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion
        //Pobieranie z bazy danych listy wszystkich klas istniejących w bazie danych
        #region Klasy
        public List<ClassModel> GetClasses()
        {
            List<ClassModel> klasy = new List<ClassModel>();
            var query = db.Klasy.ToList();
            foreach(var item in query)
            {
                klasy.Add(new ClassModel
                {
                    IdKlasy = item.IdKlasy,
                    IdWychowawcy = item.IdWychowawcy,
                    Oddział = item.Oddział,
                    //Nauczyciele = this.GetTeachers()
                });
            }
            return klasy;
        }
        public ClassModel GetClass(int IdKlasy)
        {
            ClassModel model = new ClassModel();
            var query = db.Klasy.Where(x => x.IdKlasy == IdKlasy).FirstOrDefault();
            model.IdKlasy = query.IdKlasy;
            model.Oddział = query.Oddział;
            model.IdWychowawcy = query.IdWychowawcy;
            model.Nauczyciele = this.GetTeachers();
            return model;
        }
        public void CreateClass(ClassModel model)
        {
            db.Klasy.Add(new Klasy
                {
                    IdKlasy = db.Klasy.Max(x=>x.IdKlasy) +1,
                    IdWychowawcy = model.IdWychowawcy,
                    Oddział = model.Oddział
                });
            db.SaveChanges();
        }
        public void UpdateClass(ClassModel model)
        {
            var query = db.Klasy.Where(x => x.IdKlasy == model.IdKlasy).FirstOrDefault();
            query.IdKlasy = model.IdKlasy;
            query.IdWychowawcy = model.IdWychowawcy;
            query.Oddział = model.Oddział;
            db.SaveChanges();
        }
        public void DeleteClass(ClassModel model)
        {
            var students = db.Uczniowie.Where(x => x.IdKlasy == model.IdKlasy).ToList();
            foreach (var student in students)
            {
                student.IdKlasy = 0;
            }
            var query = db.Klasy.Where(x => x.IdKlasy == model.IdKlasy).FirstOrDefault();
            db.Klasy.Remove(query);
            db.SaveChanges();
        }
        #endregion
        #region Uczniowie
        //Pobieranie listy uczniów 
        public List<StudentModel> GetStudents()
        {
            List<StudentModel> students = new List<StudentModel>();
            var query = db.Uczniowie.ToList();
            foreach(var item in query)
            {
                students.Add(new StudentModel
                    {
                        IdUcznia = item.IdUcznia,
                        IdKlasy = item.IdKlasy,
                        Login = item.Login,
                        Password = item.Hasło,
                        Name = item.Imię,
                        Surname = item.Nazwisko,
                        Klasa = db.Klasy.Where(m=> m.IdKlasy == item.IdKlasy).Select(m => m.Oddział).FirstOrDefault()
                    });
            }
            return students;
        }

        //Tworzenie nowego wpisu o uczniu w bazie danych 
        public void CreateNewStudent(StudentModel model)
        {
            db.Uczniowie.Add(new Uczniowie
            {
                IdUcznia = db.Uczniowie.Max(x =>x.IdUcznia)+1,
                Login = model.Login,
                Hasło = model.Password,
                Imię = model.Name,
                Nazwisko = model.Surname,
                IdKlasy = model.IdKlasy
            });
            db.SaveChanges();
        }
        //Pobieranie rekordu o studencie po Identyfikatorze
        public StudentModel GetStudent(int IdUcznia)
        {

            var uczen = db.Uczniowie.Where(x => x.IdUcznia == IdUcznia).FirstOrDefault();

            StudentModel model = new StudentModel();
            model.IdKlasy = uczen.IdKlasy;
            model.IdUcznia = uczen.IdUcznia;
            model.Klasa = db.Klasy.FirstOrDefault(x => x.IdKlasy == uczen.IdKlasy).Oddział;
            model.Login = uczen.Login;
            model.Password = uczen.Hasło;
            model.Name = uczen.Imię;
            model.Surname = uczen.Nazwisko;
            return model;
        }
        //Pobieranie rekordu o studencie po informacjach
        public StudentModel GetStudent(string Login)
        {
            
            var item = db.Uczniowie.Where(x => x.Login == Login).FirstOrDefault();
            StudentModel model = new StudentModel();
            model.IdKlasy = item.IdKlasy;
            model.IdUcznia = item.IdUcznia;
            model.Klasa = db.Klasy.FirstOrDefault(x => x.IdKlasy == item.IdKlasy).Oddział;
            model.Login = item.Login;
            model.Password = item.Hasło;
            model.Name = item.Imię;
            model.Surname = item.Nazwisko;
            return model;
        }
        //Kasowanie rekordu studenta na podstawie modelu
        public void DeleteStudent(StudentModel model)
        {
            var studentRef = db.Rozwiązania.Where(x => x.IdUcznia == model.IdUcznia).ToList();
            db.Rozwiązania.RemoveRange(studentRef);
            var item = db.Uczniowie.Where(x => x.IdUcznia == model.IdUcznia).FirstOrDefault();
            db.Uczniowie.Remove(item);
            db.SaveChanges();
        }
        //Aktualizacja rekordu o studencie na podstawie modelu
        public void UpdateStudent(StudentModel model)
        {
            var query = db.Uczniowie.Where(x => x.IdUcznia == model.IdUcznia).FirstOrDefault();
            query.IdUcznia = model.IdUcznia;
            query.IdKlasy = model.IdKlasy;
            query.Hasło = model.Password;
            query.Login = model.Login;
            query.Imię = model.Name;
            query.Nazwisko = model.Surname;
            db.SaveChanges();
        }
        //Aktualizacja po zmianie hasła
        public void ChangePassword(string login,string name,string surname,string newPassword)
        {
            if(ExistsStudentInDatabase(login,name,surname))
            {
                var query = db.Uczniowie.Where(x => x.Login == login && x.Imię == name && x.Nazwisko == surname).FirstOrDefault();
                {
                    query.Hasło = newPassword;
                    db.SaveChanges();
                }
            }
            else
            {
                if(ExistsTeacherInDatabase(login,name,surname))
                {
                    var query = db.Nauczyciele.Where(x => x.Login == login && x.Imię == name && x.Nazwisko == surname).FirstOrDefault();
                    {
                        query.Hasło = newPassword;
                        db.SaveChanges();
                    }
                }
            }
        }
        public bool ExistsStudentInDatabase(string login,string name,string surname)
        {
            var query = db.Uczniowie.Where(x =>x.Login == login && x.Imię == name && x.Nazwisko == surname).FirstOrDefault();
            return query != null ? true : false;  
        }
        #endregion
        #region Nauczyciele
        //Pobranie listy nauczycieli z bazy danych
        public List<TeacherModel> GetTeachers()
        {
            List<TeacherModel> Teachers = new List<TeacherModel>();
            var query = db.Nauczyciele.ToList();
            foreach (var item in query)
            {
                Teachers.Add(new TeacherModel
                {
                    IdNauczyciela = item.IdProwadzącego,
                    Login = item.Login,
                    Hasło = item.Hasło,
                    Imię = item.Imię,
                    Nazwisko = item.Nazwisko,
                    Wizytowka = item.Imię + " " +item.Nazwisko
                });
            }
            return Teachers;
        }
        //Pobranie listy nauczyciela z bazy danych na podstawie Id
        public TeacherModel GetTeacher(int IdNauczyciela)
        {
            TeacherModel Teacher = new TeacherModel();
            var query = db.Nauczyciele.Where(x =>x.IdProwadzącego == IdNauczyciela).FirstOrDefault();
            Teacher.Imię = query.Imię;
            Teacher.Login = query.Login;
            Teacher.Nazwisko = query.Nazwisko;
            Teacher.Hasło = query.Hasło;
            return Teacher;
        }
        public TeacherModel GetTeacher(string Login)
        {
            var item = db.Nauczyciele.Where(x =>x.Login == Login).FirstOrDefault();
            TeacherModel model = new TeacherModel();
            model.IdNauczyciela = item.IdProwadzącego;
            model.Imię= item.Imię;
            model.Nazwisko = item.Nazwisko;
            model.Login = item.Login;
            model.Hasło = item.Hasło;
            return model;
        }
        //Stworzenie nowego rekordu nauczyciela na podstawie bazy danych
        public void CreateNewTeacher(string Login, string Password, string Name, string Surname)
        {
            db.Nauczyciele.Add(new Nauczyciele
            {
                IdProwadzącego = db.Nauczyciele.Max(x => x.IdProwadzącego)+1,
                Login = Login,
                Hasło = Password,
                Imię = Name,
                Nazwisko = Surname
            }
            );
            db.SaveChanges();
        }

        //Aktualizacja występującego rekordu o nauczycielu
        public void UpdateTeacher(TeacherModel model)
        {
            var nauczyciel = db.Nauczyciele.Where(n => n.IdProwadzącego == model.IdNauczyciela).FirstOrDefault();
            nauczyciel.Login = model.Login;
            nauczyciel.Imię = model.Imię;
            nauczyciel.Nazwisko = model.Nazwisko;
            db.SaveChanges();
        }
        public void DeleteTeacher(TeacherModel model)
        {
            var classRef = db.Klasy.Where(x => x.IdWychowawcy == model.IdNauczyciela).ToList();
            db.Klasy.RemoveRange(classRef);
            var courseRef = db.Kursy.Where(x => x.IdProwadzącego == model.IdNauczyciela).ToList();
            foreach (var course in courseRef)
            {
                var taskRef = db.Zadania.Where(x => x.IdKursu == course.IdKursu).ToList();
                foreach(var task in taskRef)
                {
                    var solutionRef = db.Rozwiązania.Where(x => x.IdZadania == task.IdZadania).ToList();
                    db.Rozwiązania.RemoveRange(solutionRef);
                }
                db.Zadania.RemoveRange(taskRef);
            }
            db.Kursy.RemoveRange(courseRef);
            var item = db.Nauczyciele.Where(x => x.IdProwadzącego == model.IdNauczyciela).FirstOrDefault();
            db.Nauczyciele.Remove(item);
            db.SaveChanges();
        }
        public bool ExistsTeacherInDatabase(string login, string name, string surname)
        {
            var query = db.Nauczyciele.Where(x => x.Login == login && x.Imię == name && x.Nazwisko == surname);
            return query != null ? true : false;
        }
    }
}
        #endregion