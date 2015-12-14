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
                var query = db.Uczniowie.Where(x => x.Login == Login && x.Hasło == Password).FirstOrDefault();
                if (query != null)
                {
                    UserLoginModel ulm = new UserLoginModel
                    {
                        Login = query.Login,
                        Name = query.Imię,
                        Surname = query.Nazwisko,
                        Password = query.Hasło
                    };
                    return ulm;
                }
                else
                {
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
                    Oddział = item.Oddział
                });
            }
            return klasy;
        }
        #endregion
        #region Uczniowie
        /* Do zrobienia 
                - szyfrowanie hasła
         */
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
        public StudentModel GetStudent(string Name, string Surname,string Login)
        {
            var item = db.Uczniowie.Where(x => x.Imię == Name && x.Nazwisko == Surname && x.Login == Login).FirstOrDefault();
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
                    Nazwisko = item.Nazwisko
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
        public void UpdateTeacher(int IdNauczyciela)
        {
            Nauczyciele nauczyciel = db.Nauczyciele.ElementAt(IdNauczyciela);
        }
    }
}
        #endregion