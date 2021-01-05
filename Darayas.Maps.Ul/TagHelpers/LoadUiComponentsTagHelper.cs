using Darayas.Maps.Ul.Data;
using Darayas.Maps.Ul.Funcs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Darayas.Maps.Ul.TagHelpers
{
    [HtmlTargetElement("LoadUiComponents")]

    public class LoadUiComponentsTagHelper : TagHelper
    {
        public string ComponentName { get; set; }
        public string Class { get; set; }
        public string UiUrl { get; set; }
        public object Model { get; set; }
        public HttpContext httpContext { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            try
            {
                string Url = AppSettings.Get().SiteUrl + UiUrl;
                var HtmlData = await new ComponentWapper().GetDataAsync(Url, Model, httpContext.Request.Headers);

                output.TagName = "div";
                if (ComponentName != null)
                    output.Attributes.SetAttribute("id", ComponentName);
                if (Class != null)
                    output.Attributes.SetAttribute("class", Class);
                output.Content.SetHtmlContent(HtmlData);
            }
            catch (Exception ex)
            {
                output.TagName = "div";
                output.Attributes.SetAttribute("id", ComponentName);
                output.Content.SetHtmlContent("<err500>Module Error: 500, Send Information to Administrator</err500>");
            }
        }
    }
}
