using Darayas.Maps.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Darayas.Maps.DAL.Models
{
    public class tblPalceCategoris : IEntity
    {
        public string Id { get; set; }
        public string ImgName { get; set; }
        public string Title { get; set; }

        public virtual ICollection<tblPalces> tblPalces { get; set; }
    }
}
