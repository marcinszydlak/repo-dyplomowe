using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataServices.ViewModels
{
    public class UserLoginModel
    {
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Login")]
        public string Login { get; set; }
    }
}
