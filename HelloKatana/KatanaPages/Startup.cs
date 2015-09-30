using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KatanaPages
{
    using System.Runtime.InteropServices.ComTypes;
    using System.Threading.Tasks;
    using System.Web.Mvc.Html;

    using Microsoft.Owin;

    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.Use(typeof(Middleware1));
            appBuilder.Use<Middleware2>();
            appBuilder.Use(
                async (context, next) =>
                    {
                        await context.Response.WriteAsync("middleware 3 way in");
                        await next();
                        await context.Response.WriteAsync("middleware 3 way back");
                    });
            appBuilder.Map("/siva", this.C);
        }

        public void C(IAppBuilder appBuilder)
        {
            appBuilder.Use(
                async (context, next) =>
                    {

                        await context.Response.WriteAsync("middleware 4 way in");
                        //await next();
                        //context.Response.StatusCode = 200;
                        await context.Response.WriteAsync("middleware 4 way back");
                    });
        }
    }

    public class Middleware1
    {
        private Func<IDictionary<string, object>, Task> next;

        public Middleware1(Func<IDictionary<string, object>, Task> next)
        {
            this.next = next;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            var context = new OwinContext(env);

            await context.Response.WriteAsync("middlware 1 way in : ");
            await this.next(env);
            await context.Response.WriteAsync("middleware 1 way back: ");
        }
    }

    public class Middleware2
    {
        private readonly Func<IDictionary<string, object>, Task> next;

        public Middleware2(Func<IDictionary<string, object>, Task> next)
        {
            this.next = next;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            var context = new OwinContext(env);
            await context.Response.WriteAsync("Middleware 2 ");
            await this.next(env);
            await context.Response.WriteAsync("middleware 2 way back: ");
        }
    }
}