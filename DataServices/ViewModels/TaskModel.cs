using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.ViewModels
{
    public class TaskModel
    {
        [Display(Name = "Identyfikator zadania")]
        public int TaskId { get; set; }

        [Display(Name = "Identyfikator kursu")]
        public int CourseId { get; set; }

        [Display(Name = "Opis")]
        public string Descriprion { get; set; }

        [Display(Name = "Data stworzenia")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Termin oddania")]
        public DateTime DeadLineDate { get; set; }
    }
}
