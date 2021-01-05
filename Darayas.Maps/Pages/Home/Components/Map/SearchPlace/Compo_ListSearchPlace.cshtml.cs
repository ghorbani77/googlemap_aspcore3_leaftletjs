using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Darayas.Maps.DAL.Models;
using Darayas.Maps.DAL.Repository;
using Darayas.Maps.Logger;
using Darayas.Maps.Models.ViewInputs;
using Darayas.Maps.Models.ViewModels;
using Darayas.Maps.Ul.Funcs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Darayas.Maps.Pages.Home.Components.Map.SearchPlace
{
    public class Compo_ListSearchPlaceModel : PageModel
    {
        NLog.Logger logger;
        public Compo_ListSearchPlaceModel()
        {
            logger = NLogger.Instance.GetCurrentClassLogger();
            Data = new List<vmCompo_ListSearch>();
            Input = new viCompo_ListSearch();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                #region واکشی مکان ها از دیتابیس
                SqlRepository<tblPalces> repPalce = new SqlRepository<tblPalces>();

                double _NorthEastLat = double.Parse(Input.NorthEast.Split(new char[] { ',' })[0].Trim());
                double _NorthEastLng = double.Parse(Input.NorthEast.Split(new char[] { ',' })[1].Trim());

                double _SouthWestLat = double.Parse(Input.SouthWest.Split(new char[] { ',' })[0].Trim());
                double _SouthWestLng = double.Parse(Input.SouthWest.Split(new char[] { ',' })[1].Trim());

                var qLocalPlace = await repPalce.Get()
                    //.Where(a => a.Zoom <= Input.Zoom)
                    .Where(a=>a.Name.Contains(Input.Address.Trim()))
                    .Where(a => a.Lat <= _NorthEastLat)
                    .Where(a => a.Lng <= _NorthEastLng)
                    .Where(a => a.Lat >= _SouthWestLat)
                    .Where(a => a.Lng >= _SouthWestLng)
                    .Select(a => new vmCompo_ListSearch
                    {
                        ImgName = a.tblPalceCategoris.ImgName,
                        Lat = a.Lat,
                        Lng = a.Lng,
                        Name = a.Name,
                        Zoom = a.Zoom
                    })
                    .OrderBy(a => a.Name)
                    .Skip(0)
                    .Take(5)
                    .ToListAsync();

                Data.AddRange(qLocalPlace);
                #endregion

                #region واکشی اطلاعات از سرویس دهنده ها
                var _data = await new ComponentWapper().GetDataAsync("http://photon.komoot.de/api/", new { q = Input.Address, limit = 5 });

                var JsonData = JObject.Parse(_data);
                var qData = JsonData["features"].Select(a => new vmCompo_ListSearch
                {
                    Lng = double.Parse(a["geometry"]["coordinates"][0].ToString()),
                    Lat = double.Parse(a["geometry"]["coordinates"][1].ToString()),
                    Name = (a["properties"]["state"] == null ? "" : (a["properties"]["state"] + ", ")) + a["properties"]["name"],
                    Zoom = 13,
                    ImgName = "marker-here.png"
                }).ToList();

                Data.AddRange(qData);
                #endregion

                return Page();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_ListSearch Input { get; set; }
        public List<vmCompo_ListSearch> Data { get; set; }
    }
}