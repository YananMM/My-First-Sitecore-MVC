using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Sitecore.Diagnostics;
using Sitecore.Events;
using Sitecore.Security.Accounts;

namespace Landmark.Classes.EventHandlers
{
    public class UserEvent
    {
        public void OnUserCreated(object sender, EventArgs args)
        {
            var user = Event.ExtractParameter(args, 0) as MembershipUser;
            //MembershipUser muser = Membership.GetUser(user.DisplayName);
            //string password = muser.GetPassword();
            Log.Info("yanan:"+user.GetPassword(),this);
            //Log.Info("yananp:" + user.DisplayName, this);
            return;
        }
    }
}