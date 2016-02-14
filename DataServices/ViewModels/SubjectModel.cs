using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.ViewModels
{
    public class SubjectModel
    {

        [Display(Name = "Identyfikator Przedmiotu")]
        public int SubjectId { get; set; }

        [Display(Name = "Nazwa przedmiotu")]
        public string SubjectName { get; set; }
    }
}
