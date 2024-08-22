using System.Web.Http;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var config = new XmlLoggingConfiguration(Server.MapPath("~/nlog.config"));
            LogManager.Configuration = config;

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
