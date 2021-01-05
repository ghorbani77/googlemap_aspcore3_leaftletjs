using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Darayas.Maps.Models.Others
{
    public class JsResult : ContentResult
    {
        public JsResult(string script)
        {
            this.Content = script;
            this.ContentType = "application/javascript";
        }
    }
}
