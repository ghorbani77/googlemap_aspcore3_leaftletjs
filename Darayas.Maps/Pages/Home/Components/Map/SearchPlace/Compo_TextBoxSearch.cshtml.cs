using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Darayas.Maps.Models.ViewInputs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Darayas.Maps.Pages.Home.Components.Map.SearchPlace
{
    public class Compo_TextBoxSearchModel : PageModel
    {
        public void OnGet()
        {

        }

        public viCompo_TextBoxSearch Input { get; set; }
    }
}