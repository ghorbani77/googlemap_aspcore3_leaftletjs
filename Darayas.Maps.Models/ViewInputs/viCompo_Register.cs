using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Darayas.Maps.Models.ViewInputs
{
    public class viCompo_Register
    {
        [Display(Name = "ایمیل")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} را وارد نمایید")]
        [EmailAddress(ErrorMessage = "قالب ایمیل اشتباه است")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} را وارد نمایید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} را وارد نمایید")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="کلمه عبور با تکرار آن برابر نیست")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "نام")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} را وارد نمایید")]
        [StringLength(100,ErrorMessage ="{0} حداکثر میتواند {1} کاراکتر باشد")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} را وارد نمایید")]
        [StringLength(100, ErrorMessage = "{0} حداکثر میتواند {1} کاراکتر باشد")]
        public string LastName { get; set; }

    }
}
