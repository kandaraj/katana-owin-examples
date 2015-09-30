using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloKatana.Middleware
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.Owin;
    using Microsoft.Owin.Logging;

    public class LoggerMiddleware : OwinMiddleware
    {
        
        public LoggerMiddleware(OwinMiddleware next)
            : base(next)
        {
            
        }

        public override async Task Invoke(IOwinContext context)
        {
            Debug.WriteLine("middleware begin");
            await this.Next.Invoke(context);
            Debug.WriteLine("middleware end");
        }
    }
}