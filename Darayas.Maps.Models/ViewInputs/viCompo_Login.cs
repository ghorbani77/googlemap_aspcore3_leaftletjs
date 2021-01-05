using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Darayas.Maps.Models.ViewInputs
{
    public class viCompo_Login
    {
        [Display(Name = "ایمیل")]
        [Required(AllowEmptyStrings =false,ErrorMessage ="{0} را وارد نمایید")]
        [EmailAddress(ErrorMessage ="قالب ایمیل اشتباه است")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} را وارد نمایید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
