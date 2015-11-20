using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Landmark.Pipelines.RenderFieldValue
{
    public class CustomReplaceFieldValue
    {
        public void Process(Sitecore.Pipelines.RenderField.RenderFieldArgs args)
        {
            if (args.FieldTypeKey == "rich text" || args.FieldTypeKey == "single-line text" || args.FieldTypeKey == "multi-line text")
            {
                if (args.FieldValue.Contains("置"))
                {
                    args.Result.FirstPart = args.Result.FirstPart.Replace("置", "<span class=\"font_meiryo\">置</span>");
                    args.Result.LastPart = args.Result.LastPart.Replace("置", "<span class=\"font_meiryo\">置</span>");
                }
            }
        }
    }
}