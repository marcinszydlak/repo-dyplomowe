using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.ViewModels
{
    public class CourseModel
    {
        public int CourseId { get; set;}
        public int SubjectId { get; set; }
        public string Subject { get; set; }
        public int TeacherId { get; set; }
        public int ClassId { get; set; }
        public string Class { get; set; }
        public string CourseTitle { get; set; }
        public string CourseDescription { get; set; }
        public List<TaskModel> Tasks { get; set; }
        public List<ClassModel> Classes { get; set; }
        public List<SubjectModel> Subjects { get; set; }
    }
}
