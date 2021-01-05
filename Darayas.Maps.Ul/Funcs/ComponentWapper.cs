using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Darayas.Maps.Ul.Funcs
{
    public class ComponentWapper
    {
        public async Task<string> GetDataAsync(string PageUrl, object model, IHeaderDictionary Headers = null)
        {
            try
            {
                #region تولید پارامترها
                var Parr = GetModelParrameter(model);
                string UrlParr = "?";
                foreach (var item in Parr)
                {
                    if (item.Val != null)
                        UrlParr += "&" + item.Title + "=" + item.Val;
                }
                #endregion

                string url = PageUrl + UrlParr.Trim(new char[] { '&' });
                HttpWebRequest objRequest = (HttpWebRequest)HttpWebRequest.Create(url);

                objRequest.ContentType = "text/html; charset=utf-8";
                objRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; InfoPath.1; .NET CLR 2.0.50727; .NET CLR 1.1.4322)";

                #region افزودن هدرها
                if (Headers != null)
                    foreach (var item in Headers)
                    {
                        if (!item.Key.StartsWith(":"))
                            if (item.Value[0] != null)
                                objRequest.Headers.Add(item.Key, item.Value.ToString());
                    }

                objRequest.Headers.Add("Accept-Charset", "ISO-8859-9,utf-8;q=0.7,*;q=0.7");

                objRequest.Headers["Accept-Encoding"] = "deflate";
                #endregion

                var resp = (HttpWebResponse)(await objRequest.GetResponseAsync());
                string Charset = resp.CharacterSet;
                Encoding encoding = Encoding.UTF8;

                StreamReader sr = new StreamReader(resp.GetResponseStream(), encoding);
                string result = await sr.ReadToEndAsync();

                sr.Close();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private List<_GetModelParrameter> GetModelParrameter(object atype)
        {
            if (atype == null) return new List<_GetModelParrameter>();
            Type t = atype.GetType();
            PropertyInfo[] props = t.GetProperties();
            List<_GetModelParrameter> LstParr = new List<_GetModelParrameter>();
            foreach (PropertyInfo prp in props)
            {
                object value = prp.GetValue(atype, new object[] { });

                if (value != null)
                {
                    // مقادیری که خود دارای چند مقدار هستند مثل ارایه ها یا لیست ها
                    if (value.GetType() == typeof(string[]))
                        foreach (var item in (string[])value)
                            if (item != null)
                                LstParr.Add(new _GetModelParrameter() { Title = prp.Name, Val = item });


                    if (value.ToString() != "0")
                        LstParr.Add(new _GetModelParrameter() { Title = prp.Name, Val = value });
                }
            }
            return LstParr;
        }

        private class _GetModelParrameter
        {
            public string Title { get; set; }
            public object Val { get; set; }
        }

    }
}
