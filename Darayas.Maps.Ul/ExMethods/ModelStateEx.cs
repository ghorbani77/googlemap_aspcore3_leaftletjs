using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Darayas.Maps.Ul.ExMethods
{
    public static class ModelStateEx
    {
        public static string GetErrors(this ModelStateDictionary ModelState, string sprator = "<br/>")
        {
            var message = string.Join(sprator, ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage));

            return message;
        }
    }
}
