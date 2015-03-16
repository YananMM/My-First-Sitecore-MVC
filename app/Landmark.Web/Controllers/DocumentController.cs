using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Landmark.Web.Models;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web.Mvc;
using Landmark.Web.Models.sitecore.templates.User_Defined.MyFirstSitecoreMVC;
using Sitecore.Data.Items;

namespace Landmark.Web.Controllers
{
    public class DocumentController : Controller
    {
        //
        // GET: /Document/

        ISitecoreContext _context;
        ISitecoreService _master;

        public ActionResult Index()
        {
            _context = new SitecoreContext();
            var model = _context.GetCurrentItem<DocumentList>();
            return View("/Views/Renderings/DocumentsControllerView.cshtml",model);
        }

    }
}
