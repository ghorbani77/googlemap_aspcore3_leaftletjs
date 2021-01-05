using Darayas.Maps.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Darayas.Maps.Ul.Data
{
    public static class AppSettings
    {
        private static vmAppSettings Data { get; set; }

        public static void Set(vmAppSettings _Data)
        {
            Data = _Data;
        }

        public static vmAppSettings Get()
        {
            return Data;
        }
    }
}
