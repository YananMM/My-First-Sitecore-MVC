using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.WebPages;
using System.Web.WebPages.Instrumentation;

namespace Landmark.Classes
{
    public class CustomReplacePreventer : IDisposable
    {
        private readonly WebPageExecutingBase _page;
        public CustomReplacePreventer(WebPageExecutingBase page)
        {
            _page = page;
            _page.Context.Items["CustomReplace"] = false;
        }

        public void Dispose()
        {
            _page.Context.Items["CustomReplace"] = true;
        }
    }

    public abstract class WebViewPage : System.Web.Mvc.WebViewPage
    {
        
    }

    public abstract class WebViewPage<T> : System.Web.Mvc.WebViewPage<T>
    {
        public bool CustomReplace
        {
            get
            {
                return Context.Items.Contains("CustomReplace")
                    && Context.Items["CustomReplace"] is bool
                    && (bool)Context.Items["CustomReplace"];
            }

            set
            {
                Context.Items["CustomReplace"] = value;
            }
        }
        public void EnableCustomReplace()
        {
            CustomReplace = true;
        }
        public void DisableCustomReplace()
        {
            CustomReplace = false;
        }

        private static IHtmlString DoCustomReplace(IHtmlString value)
        {
            var s = value.ToString();
            if (!s.Contains("置地"))
                return value;

            var startIndex = 0;
            var endIndex = s.Length;
            if (s.StartsWith("<a "))
            {
                startIndex = s.IndexOf(">") + 1;
                endIndex = s.IndexOf("</a>");
            }

            return new MvcHtmlString(s.Substring(0, startIndex)
                                + s.Substring(startIndex, endIndex - startIndex)
                                    .Replace("置地", "<span class=\"font_meiryo\">置</span>地")
                                + s.Substring(endIndex));
        }


        public CustomReplacePreventer BeginPreventCustomReplace()
        {
            return new CustomReplacePreventer(this);
        }

        public MvcHtmlString PreventCustomReplace(object value)
        {
            // replace to temporary string
            return new MvcHtmlString(value.ToString().Replace("置地", "*置*地"));
        }
        
        public override void Write(object value)
        {
            if (value is IHtmlString)
            {
                if (VirtualPath.StartsWith("/Views/Layouts"))
                {
                    // in root layout, replace the temporary string back
                    value = new MvcHtmlString(value.ToString().Replace("*置*地", "置地"));
                }
                else if (CustomReplace)
                {
                    value = DoCustomReplace(value as IHtmlString);
                }
                else
                {
                    value = PreventCustomReplace(value);
                }
            }
            base.Write(value);
        }


        /*public override void WriteLiteral(object value)
        {
            if (CustomReplace && value is IHtmlString)
            {
                value = DoCustomReplace(value as IHtmlString);
            }
            base.WriteLiteral(value);
        }*/

        /*public override void WriteAttribute(string name, PositionTagged<string> prefix, PositionTagged<string> suffix, params AttributeValue[] values)
        {
            if (CustomReplace)
            {
                //Output.Write("\nWriteAttribute[[ name:" + name + " prefix:" + prefix.Value + " suffix:" + suffix.Value + "values:" + string.Join(",", values.Select(v => v.Value)) + "\n");
            }
            base.WriteAttribute(name, prefix, suffix, values);
        }*/
        
    }
}