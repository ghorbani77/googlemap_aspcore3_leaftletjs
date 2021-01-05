using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Darayas.Maps.Logger;
using Darayas.Maps.Models.ViewInputs;
using Darayas.Maps.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Darayas.Maps.Pages.Home.Components.Map.SearchPlace
{
    public class Compo_SearchModel : PageModel
    {
        NLog.Logger logger;
        public Compo_SearchModel()
        {
            logger = NLogger.Instance.GetCurrentClassLogger();
            Input = new viCompo_Search();
            Data = new vmCompo_Search();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {

                return Page();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return StatusCode(500);
            }
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Search Input { get; set; }
        public vmCompo_Search Data { get; set; }
    }
}