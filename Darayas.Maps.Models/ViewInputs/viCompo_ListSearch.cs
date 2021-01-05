using System;
using System.Collections.Generic;
using System.Text;

namespace Darayas.Maps.Models.ViewInputs
{
    public class viCompo_ListSearch
    {
        public string Address { get; set; }
        public string NorthEast { get; set; }
        public string SouthWest { get; set; }
        public int Zoom { get; set; }
    }
}
