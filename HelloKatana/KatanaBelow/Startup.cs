using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KatanaBelow
{
    using System.Threading.Tasks;

    using Microsoft.Owin;

    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.Use(typeof(Middleware1));
            appBuilder.Use<Middleware2>();
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
        private Func<IDictionary<string, object>, Task> next;

        public Middleware2(Func<IDictionary<string, object>, Task> next)
        {
            this.next = next;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            var context = new OwinContext(env);
            await context.Response.WriteAsync("Middleware 2 ");
            /*foreach (var e in env.Keys)
            {
                Console.WriteLine("{0} : {1}", env[e], e);
            }*/
            await Task.FromResult(0);
        }
    }

}