using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UnluCo.NetBootcamp.Odev5.Services;

namespace UnluCo.NetBootcamp.Odev5.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;//Kendinden sonraki middleware aktif edebilmesi icin RequestDelegate den faydalanir
        private readonly ILoggerService _loggerService;
        public GlobalExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }
        public async Task InvokeAsync(HttpContext context)//async ve await metotlarin geri dönüs tipi Task olur.
        {
            var watch = Stopwatch.StartNew();//zamani hesaplamak icin sayac baslatildi
            try
            {
                string message = "[Request] Http " + context.Request.Method + " - " + context.Request.Path + " - " + DateTime.Now;
                _loggerService.Write(message);
                await _next(context);//kendinden sonraki middleware cagirir, _next.Invoke(context) ile ayni islevde
                watch.Stop();
                message = "[Response] Http " + context.Request.Method + " - " + context.Request.Path + " - " + context.Response.StatusCode + " - response time: " + watch.Elapsed.TotalMilliseconds + "ms";
                _loggerService.Write(message);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleExeption(context, ex, watch);
            }
        }
        private Task HandleExeption(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType = "application/json"; //genelde json formatinda olur
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;//500 hata kodu doner

            string message = "[Error] Http " + context.Request.Method + " - " + context.Response.StatusCode +
                " - Error Message : " + ex.Message + " - response time: " + watch.Elapsed.TotalMilliseconds + "ms";
            _loggerService.Write(message);

            var result = JsonConvert.SerializeObject(new { error = "InternalServerError" }, Formatting.None);//donus formatini json olarak verir
            return context.Response.WriteAsync(result);
        }
    }
    public static class GlobalExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)//app. ile cagrilabilmesi icin app builder kullanildi.
        {
            return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}