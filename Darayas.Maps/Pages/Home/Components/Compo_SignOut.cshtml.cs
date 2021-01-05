using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Darayas.Maps.DAL.Models;
using Darayas.Maps.Logger;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Darayas.Maps.Pages.Home.Components
{
    public class Compo_SignOutModel : PageModel
    {

        private readonly UserManager<tblUsers> _userManager;
        private readonly SignInManager<tblUsers> _signInManager;
        NLog.Logger logger = null;

        public Compo_SignOutModel(UserManager<tblUsers> userManager, SignInManager<tblUsers> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

            logger = NLogger.Instance.GetCurrentClassLogger();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await _signInManager.SignOutAsync();
            return Page();
        }
    }
}