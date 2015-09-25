using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using MachoManFFStats.BLL;
using MachoManFFStats.Data.Entity;
using MachoManFFStats.Data.ServiceImplementation;
using MachoManFFStats.WebApi.Secrets;
using Newtonsoft.Json.Serialization;

namespace MachoManFFStats.WebApi
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //create builder and RegisterControllers with builder
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //Register Interfaces with their implementations
            var connString = EnvironmentSettings.GetConnectionString();
            builder.RegisterType<MachoManFFEntities>()
                .WithParameter("connString", connString)
                .InstancePerRequest();

            builder.RegisterType<StandingsDbService>().As<IStandingsService>();
            builder.RegisterType<MatchupsDbService>().As<IMatchupsService>();
            builder.RegisterType<TeamsDbService>().As<ITeamsService>();

            //Configure JsonSerializer to camelCase Data
            var httpConfiguration = GlobalConfiguration.Configuration;
            var jsonSerializer = httpConfiguration.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonSerializer.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //build builder and set DependencyResolver for Http Application
            var container = builder.Build();

            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}