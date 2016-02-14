using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace DataServices.ViewModels
{
    public class CourseModel
    {
        [Display(Name = "Identyfikator kursu")]
        public int CourseId { get; set;}

        [Display(Name = "Identyfikator przedmiotu")]
        public int SubjectId { get; set; }

        [Display(Name = "Nazwa przedmiotu")]
        public string Subject { get; set; }

        [Display(Name = "Identyfikator nauczyciela")]
        public int TeacherId { get; set; }

        [Display(Name = "Identyfikator klasy")]
        public int ClassId { get; set; }

        [Display(Name = "Nazwa klasy")]
        public string Class { get; set; }

        [Display(Name = "Nazwa kursu")]
        public string CourseTitle { get; set; }

        [Display(Name = "Opis kursu")]
        public string CourseDescription { get; set; }

        public List<TaskModel> Tasks { get; set; }

        public List<ClassModel> Classes { get; set; }

        public List<SubjectModel> Subjects { get; set; }
    }
}
