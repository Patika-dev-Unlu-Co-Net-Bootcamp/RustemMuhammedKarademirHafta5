using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UnluCo.NetBootcamp.Odev5.Models
{
    public class LoginUserModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı boş bırakılamaz!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Şifre boş bırakılamaz!")]
        public string Password { get; set; }
    }
}
