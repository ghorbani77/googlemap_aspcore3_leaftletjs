using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Darayas.Maps.DAL.Models;
using Darayas.Maps.Logger;
using Darayas.Maps.Models.JsMethods;
using Darayas.Maps.Models.ViewInputs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Darayas.Maps.Ul.ExMethods;

namespace Darayas.Maps.Pages.Home.Components
{
    public class Compo_RegisterModel : PageModel
    {
        private readonly UserManager<tblUsers> _userManager;
        private readonly SignInManager<tblUsers> _signInManager;
        NLog.Logger logger = null;

        public Compo_RegisterModel(UserManager<tblUsers> userManager, SignInManager<tblUsers> signInManager)
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

                tblUsers tUser = new tblUsers()
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Name = Input.FirstName,
                    Family = Input.LastName,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(tUser, Input.Password);
                if (result.Succeeded)
                {
                    return MsgBox.ShowSuccessMsg("RefreshPage");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }

                    return MsgBox.ShowModelStateMsg(ModelState.GetErrors());
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return MsgBox.ShowErr500Msg();
            }
        }

        [BindProperty]
        public viCompo_Register Input { get; set; }
    }
}