using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Darayas.Maps.Logger;
using Darayas.Maps.Models.ViewInputs;
using Darayas.Maps.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace Darayas.Maps.Pages.Home.Components.Map.Directions
{
    public class Compo_GetLstRoadsModel : PageModel
    {
        NLog.Logger logger;
        public Compo_GetLstRoadsModel()
        {
            logger = NLogger.Instance.GetCurrentClassLogger();
        }
        public IActionResult OnPost()
        {
            try
            {
                string _Data = Input.JsonData.Trim(new char[] { '[' }).Trim(new char[] { ']' });
                var JsonData = JObject.Parse(_Data);


                var qData = from a in JsonData["instructions"]
                            select new vmCompo_GetLstRoads
                            {
                                Road = a["road"].ToString(),
                                Text = a["text"].ToString(),
                                Dist = double.Parse(a["distance"].ToString()),
                                Time = double.Parse(a["time"].ToString()),
                            };

                Data = qData.ToList();

                return Page();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return StatusCode(500);
            }
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_GetLstRoads Input { get; set; }
        public List<vmCompo_GetLstRoads> Data { get; set; }
    }
}