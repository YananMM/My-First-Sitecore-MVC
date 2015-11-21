using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.WebPages;
using System.Web.WebPages.Instrumentation;

namespace Landmark.Classes
{

    public abstract class WebViewPage : System.Web.Mvc.WebViewPage
    {
        
    }

    public abstract class WebViewPage<T> : System.Web.Mvc.WebViewPage<T>
    {
        public bool CustomReplace;
        public override void Write(object value)
        {
            if (CustomReplace && value is IHtmlString)
            {
                //Output.Write("\nWrite object[[value:" + value + "\n");
                value = new HtmlString(value.ToString().Replace("置地", "<span class=\"font_meiryo\">置</span>地"));
            }
            base.Write(value);
        }


        public override void WriteLiteral(object value)
        {
            if (CustomReplace && value is IHtmlString)
            {
                //Output.Write("\nWriteLiteral[[value:" + value +"\n");
                value = new HtmlString(value.ToString().Replace("置地", "<span class=\"font_meiryo\">置</span>地"));
            }
            base.WriteLiteral(value);
        }

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