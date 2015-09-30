
namespace HelloKatana
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HelloKatana.Middleware;

    using Microsoft.Owin;

    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Run(Handler);
            app.Use<LoggerMiddleware>(new TraceLogger());

        }

        private static Task Handler(IOwinContext context)
        {
            context.Response.ContentType = "text/plain";
            return context.Response.WriteAsync("Hello Katana");
        }
    }

    public class TraceLogger : LoggerMiddleware
    {
        public TraceLogger(OwinMiddleware next)
            : base(next)
        {
        }
    }
}