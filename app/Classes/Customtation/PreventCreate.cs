using Sitecore.Data.Events;
using Sitecore.Data.Items;
using Sitecore.Events;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Landmark.Classes.Customtation
{
    public class PreventCreate
    {
        public void OnItemCreating(object sender, EventArgs args)
        {
            if (Sitecore.Context.Job.Name=="Publish to 'web'")
                return;
            using (new SecurityDisabler())
            {
                if (args == null)
                    return;
                else
                {
                    ItemCreatingEventArgs creatingArg = Event.ExtractParameter(args, 0) as ItemCreatingEventArgs;
                    Item parent = creatingArg.Parent;
                    string name = creatingArg.ItemName;
                    if (parent != null && !string.IsNullOrEmpty(name))
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