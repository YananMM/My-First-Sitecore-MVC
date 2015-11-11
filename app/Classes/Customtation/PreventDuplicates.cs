using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Events;
using Sitecore.Data.Items;
using Sitecore.Events;
using Sitecore.Mvc.Extensions;
using Sitecore.SecurityModel;

namespace Landmark.Classes.Customtation
{
    public class PreventDuplicates
    {
        public void OnItemCopying(object sender, EventArgs args)
        {
            if (Sitecore.Context.Job != null)
            {
                if (Sitecore.Context.Job.Name == "Publish to 'web'")
                    return;
                if (Sitecore.Context.Job.Name == "Install")
                    return;
            }
            using (new SecurityDisabler())
            {
                if(args == null)
                    return;
                else
                {
                    Item source = Event.ExtractParameter(args, 0) as Item;
                    Item parent = Event.ExtractParameter(args, 1) as Item;
                    string name = Event.ExtractParameter(args, 2) as string;
                    if(parent !=null && !name.IsEmptyOrNull())
                    foreach (Item currentItem in parent.GetChildren())
                    {
                        if ((name.ToLower() == currentItem.DisplayName.ToLower()))
                        {
                            ((SitecoreEventArgs)args).Result.Cancel = true;
                            Sitecore.Context.ClientPage.ClientResponse.Alert
                              ("Name " + currentItem.Name + " is already in use.Please use another name for the page.");
                            return;
                        }
                    }
                    
                }
            }
        }
    }
}