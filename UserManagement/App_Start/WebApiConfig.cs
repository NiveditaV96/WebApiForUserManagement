using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using UserManagement.Repository;
using UserManagement.Models;
using System.Resources;

namespace UserManagement
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var builder = new ContainerBuilder();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // To register the components with the services offered
            builder.RegisterType<UserModel>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
          
            // Set the dependency resolver to be Autofac.
            var container = builder.Build();

            //check this dot resolve all the contents of the container at a time
            // resolve as when its required
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            //ResourceManager rm = new ResourceManager("UsingRESX.UserControllerMessages",
            //    Assembly.GetExecutingAssembly());
            //String strWebsite = rm.GetString("Website", CultureInfo.CurrentCulture);
            //String strName = rm.GetString("Name");
            //form1.InnerText = "Website: " + strWebsite + "--Name: " + strName;



            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
