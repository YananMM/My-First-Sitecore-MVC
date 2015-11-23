using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Extensions;

namespace Landmark.Pipelines.ConvertToRuntimeHtml
{
    public class ReplaceSpecialChar
    {
        public void Process(Sitecore.Pipelines.ConvertToRuntimeHtml.ConvertToRuntimeHtmlArgs args)
        {
            /*var bodyPos = args.Html.IndexOf("<body>", StringComparison.InvariantCultureIgnoreCase);
            if (bodyPos == -1)
                return;
            args.Html = args.Html.Left(bodyPos) + args.Html.Substring(bodyPos).Replace("置", "<span class=\"font_meiryo\">置</span>");*/
        }
    }
}