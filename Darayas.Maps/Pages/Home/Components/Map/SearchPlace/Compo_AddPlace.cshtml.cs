using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Darayas.Maps.DAL.Models;
using Darayas.Maps.DAL.Repository;
using Darayas.Maps.Logger;
using Darayas.Maps.Models.JsMethods;
using Darayas.Maps.Models.ViewInputs;
using Darayas.Maps.Ul.ExMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Darayas.Maps.Pages.Home.Components.Map.SearchPlace
{
    public class Compo_AddPlaceModel : PageModel
    {
        NLog.Logger logger;
        public Compo_AddPlaceModel()
        {
            logger = NLogger.Instance.GetCurrentClassLogger();
        }
        public async Task<IActionResult> OnGet()
        {
            #region واکشی دسته های مکان
            LstCategoris = new List<Tuple<string, string>>();
            LstCategoris = await new SqlRepository<tblPalceCategoris>().Get()
                .Select(a => new Tuple<string, string>(a.Id, a.Title))
                .ToListAsync();
            #endregion

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                    return MsgBox.ShowModelStateMsg(ModelState.GetErrors());

                tblPalces tP = new tblPalces()
                {
                    Id = Guid.NewGuid().ToString(),
                    CateId = Input.CateId,
                    Lat = Input.Lat,
                    Lng = Input.Lng,
                    Name = Input.Name,
                    Zoom = Input.Zoom
                };

                await new SqlRepository<tblPalces>().AddAsync(tP, true);
                return MsgBox.ShowSuccessMsg("ReloadMap");
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return MsgBox.ShowErr500Msg();
            }
        }

        [BindProperty(SupportsGet =true)]
        public viCompo_AddPlace Input { get; set; }
        public List<Tuple<string, string>> LstCategoris { get; set; }

    }
}