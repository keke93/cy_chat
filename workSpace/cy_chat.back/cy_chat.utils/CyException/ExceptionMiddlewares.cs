using log4net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace cy_chat.utils
{
    public class ExceptionMiddlewares
    {
        private ILog log;
        private readonly RequestDelegate next;
        private IHostingEnvironment environment;
        public ExceptionMiddlewares(RequestDelegate next, IHostingEnvironment environment, ILogger<ExceptionMiddlewares> logger)
        {
            this.log = LogManager.GetLogger(CommonUtil.repository.Name, typeof(ExceptionMiddlewares));
            this.next = next;
            this.environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
                var features = context.Features;
            }
            catch (Exception e)
            {
                await HandleException(context, e);
            }
        }

        private async Task HandleException(HttpContext context, Exception e)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/json;charset=utf-8;";
            string error = "";

            if (environment.IsDevelopment())
            {
                var json = new { message = e.Message + "【" + e.StackTrace + "】" };
                log.Error(json);
                error = json.ToJson();
            }
            else
            {
                var json = new { message = e.Message + "【" + e.StackTrace + "】" };
                log.Error(json);
                error = json.ToJson();
                error = "抱歉，出错了";
            }


            await context.Response.WriteAsync(error);
        }
    }

    internal class Startup
    {
    }
}
