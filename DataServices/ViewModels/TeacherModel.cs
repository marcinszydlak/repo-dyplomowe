using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.ViewModels
{
    public class TeacherModel
    {
        public int IdNauczyciela { get; set; }
        public string Imię { get; set; }
        public string Nazwisko { get; set; }
        public string Login { get; set; }
        public string Hasło { get; set; }
        public string Wizytowka { get; set; }
        public override string ToString()
        {
            return this.Imię + " " + this.Nazwisko;
        }
    }
}
