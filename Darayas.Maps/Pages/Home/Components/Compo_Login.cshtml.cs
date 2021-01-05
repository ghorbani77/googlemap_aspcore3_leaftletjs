using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Darayas.Maps.DAL.Models;
using Darayas.Maps.Logger;
using Darayas.Maps.Models.JsMethods;
using Darayas.Maps.Models.Others;
using Darayas.Maps.Models.ViewInputs;
using Darayas.Maps.Ul.ExMethods;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Darayas.Maps.Pages.Home.Components
{
    public class Compo_LoginModel : PageModel
    {
        private readonly UserManager<tblUsers> _userManager;
        private readonly SignInManager<tblUsers> _signInManager;
        NLog.Logger logger = null;

        public Compo_LoginModel(UserManager<tblUsers> userManager, SignInManager<tblUsers> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

            logger = NLogger.Instance.GetCurrentClassLogger();
        }


        public IActionResult OnGet()
        {

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                    return MsgBox.ShowModelStateMsg(ModelState.GetErrors());

                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, true, true);
                if (result.Succeeded)
                {

                    return new JsResult("location.href='/'");
                }
                else if (result.IsLockedOut)
                {
                    return MsgBox.ShowModelStateMsg("حساب شما قفل شده است. لطفا 15 دقیقه دیگر مراجعه بفرمایید");
                }
                else
                {
                    return MsgBox.ShowModelStateMsg("نام کاربری یا کلمه عبور اشتباه است");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return MsgBox.ShowErr500Msg();
            }
        }

        [BindProperty]
        public viCompo_Login Input { get; set; }
    }
}