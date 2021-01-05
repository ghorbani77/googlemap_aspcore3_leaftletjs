using Darayas.Maps.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Darayas.Maps.DAL.Repository
{
    public class ConnStr
    {
        private IConnStr conn = null;
        public ConnStr()
        {
            //conn = new ServerConn(); ;
            conn = new LocalConn(); ;
        }

        public string GetConn()
        {
            return conn.Get();
        }
    }

    class ServerConn : IConnStr
    {
        public string Get()
        {
            return @""; // کانکشن استرینگ سرور
        }
    }

    class LocalConn : IConnStr
    {
        public string Get()
        {
            return "Server=.;Database=DarayasMaps;Trusted_Connection=True;"; // کانکشن استرینگ لوکال
        }
    }
}
