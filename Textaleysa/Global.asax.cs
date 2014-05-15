using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using Textaleysa.Models;

namespace Textaleysa
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

			Database.SetInitializer<ApplicationDbContext>(null);

			#region create admin
			IdentityManager manager = new IdentityManager();
			if (!manager.RoleExists("Administrators"))
			{
				manager.CreateRole("Administrators");
			}
			if (!manager.UserExists("Moss"))
			{
				ApplicationUser newAdmin = new ApplicationUser();
				newAdmin.UserName = "Moss";
				manager.CreateUser(newAdmin, "01189998819991197253");
				manager.AddUserToRole(newAdmin.Id, "Administrators");
			}
			#endregion
        }
    }
}
