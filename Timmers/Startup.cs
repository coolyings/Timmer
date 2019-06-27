using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Owin;
using Serilog;

[assembly: OwinStartup(typeof(Timmers.Startup))]

namespace Timmers
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkID=316888

            app.SetLoggerFactory(new ApiLoggerFactory());
            ServiceLog.Logger = ServiceLog.CreateLogger();
            Log.Logger = ServiceLog.Logger;
        }
    }
}
