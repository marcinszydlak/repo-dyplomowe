using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.ViewModels
{
    public class LoggedUserModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
    }
}
