using Darayas.Maps.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Darayas.Maps.DAL.Models
{
    public class tblUsers : IdentityUser, IEntity
    {
        public string Name { get; set; }
        public string Family { get; set; }
    }
}
