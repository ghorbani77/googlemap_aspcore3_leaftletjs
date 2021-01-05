using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Darayas.Maps.Ul.Funcs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace Darayas.Maps.Pages.Home.Components.Map.Directions
{
    public class Compo_LoadDirectionModel : PageModel
    {
        public void OnGet()
        {

        }

        public async Task<JsonResult> OnGetAddressAutoCompleteAsync(string term)
        {
            try
            {
                var _data = await new ComponentWapper().GetDataAsync("http://photon.komoot.de/api/", new { q = term });

                var JsonData = JObject.Parse(_data);
                var qData = JsonData["features"].Select(a => new
                {
                    id = a["geometry"]["coordinates"][1] + ", " + a["geometry"]["coordinates"][0],
                    value = (a["properties"]["state"] == null ? "" : (a["properties"]["state"] + ", ")) + a["properties"]["name"],
                    label = (a["properties"]["state"] == null ? "" : (a["properties"]["state"] + ", ")) + a["properties"]["name"]
                });

                return new JsonResult(qData);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { id = "", value = "err500", label = "err500" });
                throw;
            }
        }
    }
}