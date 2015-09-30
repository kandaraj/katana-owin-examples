using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleServer
{
    using Microsoft.Owin.Hosting;

    using Owin;

    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<ConfigStartup>("http://localhost:12345"))
            {
                Console.ReadLine();
            }
        }

    }


    public class ConfigStartup
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

        public Middleware1(Func<IDictionary<string,object>, Task> next)
        {
            this.next = next;
        }

        public async Task Invoke(IDictionary<string,object> env)
        {
            Console.Write("middlware 1 way in : ");
            Console.WriteLine(env["owin.ResponseStatusCode"]);
            /*foreach (var e in env.Keys)
            {
                Console.WriteLine("{0} : {1}", env[e], e);
            }
            */
            await this.next(env);
            env["owin.ResponseStatusCode"] = 302;
            Console.Write("middleware 1 way back: ");
            Console.WriteLine(env["owin.ResponseStatusCode"]);
        }
    }

    public class Middleware2
    {
        private Func<IDictionary<string, object>, Task> next;

        public Middleware2(Func<IDictionary<string, object>, Task> next)
        {
            this.next = next;
        }

        public Task Invoke(IDictionary<string, object> env)
        {
            Console.WriteLine("Middleware 2 ");
            /*foreach (var e in env.Keys)
            {
                Console.WriteLine("{0} : {1}", env[e], e);
            }*/
            return Task.FromResult(0);
        }
    }
}
