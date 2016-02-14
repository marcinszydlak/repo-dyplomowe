using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace DataServices.ViewModels
{
    public class ClassModel
    {
        [Display(Name = "Identyfikator klasy")]
        public int IdKlasy { get; set; }

        [Display(Name = "Nazwa klasy")]
        public string Oddział { get; set; }

        [Display(Name = "Identyfikator")]
        public int IdWychowawcy { get; set; }

        public List<TeacherModel> Nauczyciele { get; set; }
    }
}
