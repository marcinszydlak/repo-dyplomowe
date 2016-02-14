using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.ViewModels
{
    public class NoteModel
    {
        [Display(Name = "Nota")]
        public byte Value { get; set; }

        [Display(Name = "Nazwa oceny")]
        public string StringValue { get; set; }
    }
}
