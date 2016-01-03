using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.ViewModels
{
    public class TaskModel
    {
        public int TaskId { get; set; }
        public int CourseId { get; set; }
        public string Descriprion { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DeadLineDate { get; set; }
    }
}
