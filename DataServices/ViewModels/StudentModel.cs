using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataServices.ViewModels
{
    public class StudentModel
    {

        [Display(Name = "Identyfikator ucznia")]
        public int IdUcznia { get; set; }
        
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Login")]
        public string Login { get; set; }

        [Display(Name = "Identyfikator klasy")]
        public int IdKlasy { get; set; }

        [Display(Name = "Nazwa klasy")]
        public string Klasa { get; set; }
        public List<ClassModel> Oddziały { get; set; }

        public override string ToString()
        {
            return this.Name + " " + this.Surname;
        }
    }
}
