using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Authentication;
using Spring.Core.IO;
using Spring.Objects.Factory;
using Spring.Objects.Factory.Xml;
using MvcContrib.Spring;
using MvcContrib.Services;
using MvcContrib.ControllerFactories;
using Spring.Context.Support;
using Spring.Context;

namespace MVCAuthenticationSample
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.aspx/{*pathInfo}");
            routes.IgnoreRoute("Scripts/{*pathInfo}");
            routes.IgnoreRoute("Content/{*pathInfo}");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Authenticate", action = "Index", id = "" }  // Parameter defaults
            );

        }

        private void ConfigureIoC()
        {
            
            WebApplicationContext webApplicationContext = ContextRegistry.GetContext() as WebApplicationContext;
            DependencyResolver.InitializeWith(new SpringDependencyResolver(webApplicationContext.ObjectFactory));
            ControllerBuilder.Current.SetControllerFactory(typeof(IoCControllerFactory));
        }

       
        protected void Application_Start()
        {
            ConfigureIoC();
            RegisterRoutes(RouteTable.Routes);
            log4net.Config.XmlConfigurator.Configure();
        }


        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            string cookie = FormsAuthentication.FormsCookieName;
            HttpCookie httpCookie = Context.Request.Cookies[cookie];

            if (httpCookie == null) return;

            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(httpCookie.Value);
            if (ticket == null || ticket.Expired) return;

            FormsIdentity identity = new FormsIdentity(ticket);
            UserData udata = UserData.CreateUserData(ticket.UserData);
            AuthenticationProjectPrincipal principal = new AuthenticationProjectPrincipal(identity, udata);
            Context.User = principal;
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {

        }

        protected void Application_EndRequest(Object sender, EventArgs e)
        {

        }

    }
}