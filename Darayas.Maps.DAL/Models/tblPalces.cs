using Darayas.Maps.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Darayas.Maps.DAL.Models
{
    public class tblPalces :IEntity
    {
        public string Id { get; set; }
        public string CateId { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public int Zoom { get; set; }

        public virtual tblPalceCategoris tblPalceCategoris { get; set; }
    }
}
