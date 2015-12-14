using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.ViewModels
{
    public class StudentModel
    {
        public int IdUcznia { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public int IdKlasy { get; set; }
        public string Klasa { get; set; }
        public List<ClassModel> Oddziały { get; set; }

        public override string ToString()
        {
            return this.Name + " " + this.Surname;
        }
    }
}
