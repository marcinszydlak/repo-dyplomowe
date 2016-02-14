using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DataServices.ViewModels
{
    public class AdminLogin
    {
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Display(Name = "Hasło")]
        public string Password { get; set; }
    }
}
