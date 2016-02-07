using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.ViewModels
{
    public class SolutionModel
    {
        public int SolutionId { get; set; }
        public int StudentId { get; set; }
        public int TaskId { get; set; }
        public byte[] Solution { get; set; }
        public byte? Note { get; set; }
        public string Comment { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public StudentModel Student { get; set; }
    }
}
