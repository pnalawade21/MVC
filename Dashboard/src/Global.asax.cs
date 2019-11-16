using MyMVCDashboard.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Newtonsoft.Json;
using MyMVCDashboard.Models;
using MyMVCDashboard.CustomAuthentication;

namespace MyMVCDashboard
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies["Cookie1"];

            if (authCookie != null)
            {
                FormsAuthenticationTicket formTicket = FormsAuthentication.Decrypt(authCookie.Value);

                var serializeModel = JsonConvert.DeserializeObject<CustomSerializeModel>(formTicket.UserData);
                CustomPrincipal cuPrinicpal = new CustomPrincipal(formTicket.Name);
                cuPrinicpal.FirstName = serializeModel.FirstName;
                cuPrinicpal.LastName = serializeModel.LastName;
                cuPrinicpal.UserId = serializeModel.UserId;
                cuPrinicpal.Roles = serializeModel.RoleName.ToArray<String>();

                HttpContext.Current.User = cuPrinicpal;
            }
        }
    }
}
