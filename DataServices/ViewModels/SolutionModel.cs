using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataServices.ViewModels
{
    public class SolutionModel
    {
        [Display(Name = "Identyfikator rozwiązania")]
        public int SolutionId { get; set; }
        
        [Display(Name = "Identyfikator ucznia")]
        public int StudentId { get; set; }

        [Display(Name = "Identyfikator rozwiązania")]
        public int TaskId { get; set; }

        [Display(Name = "Plik z rozwiązaniem")]
        public byte[] Solution { get; set; }

        [Display(Name = "Ocena")]
        public byte? Note { get; set; }

        [Display(Name = "Komentarz")]
        public string Comment { get; set; }

        [Display(Name = "Nazwa pliku")]
        public string FileName { get; set; }

        [Display(Name = "Rozszerzenie")]
        public string Extension { get; set; }

        public StudentModel Student { get; set; }
        public List<NoteModel> Notes { get; set; }
 
    }
}
