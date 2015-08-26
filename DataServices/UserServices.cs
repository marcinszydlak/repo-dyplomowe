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

        //Pobieranie z bazy danych listy wszystkich klas istniejących w bazie danych
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
        public List<StudentModel> GetStudents()
        {
            List<StudentModel> students = new List<StudentModel>();
            var query = db.Uczniowie.ToList();
            foreach(var item in query)
            {
                students.Add(new StudentModel
                    {
                        IdUcznia = item.IdUcznia,
                        IdKlasy =item.IdKlasy,
                        Login = item.Login,
                        Password = item.Hasło,
                        Name = item.Imię,
                        Surname = item.Nazwisko,
                        Klasa = db.Klasy.Where(m=> m.IdKlasy == item.IdKlasy).Select(m=> m.Oddział).FirstOrDefault()
                    });
            }
            return students;
        }

        //Tworzenie nowego wpisu o uczniu w bazie danych 
        /* Do zrobienia 
                - szyfrowanie hasła
         */
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
        public void CreateNewTeacher(string Login,string Password,string Name,string Surname)
        {
            db.Nauczyciele.Add(new Nauczyciele
            {
                Login = Login,
                Hasło = Password,
                Imię = Name,
                Nazwisko = Surname
            }
            );
            db.SaveChanges();
        }
        public void UpdateTeacher(int IdNauczyciela)
        {
            Nauczyciele nauczyciel = db.Nauczyciele.ElementAt(IdNauczyciela);
        }
    }
}
