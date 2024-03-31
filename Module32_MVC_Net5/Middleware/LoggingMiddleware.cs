using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Module32_MVC_Net5.Repository;
using Module32_MVC_Net5.Models.Db;

namespace Module32_MVC_Net5.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        
        // ссылка на репозиторий логгера
        private readonly ILoggerRepository _repo;

        /// <summary>
        ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
        /// </summary>
        public LoggingMiddleware(RequestDelegate next, ILoggerRepository loggerRepository)
        {
            _next = next;
            _repo = loggerRepository;
        }

        /// <summary>
        ///  Необходимо реализовать метод Invoke  или InvokeAsync
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            LogConsole(context);
            LogToDB(context);

            // Передача запроса далее по конвейеру
            await _next.Invoke(context);
        }

        private void LogConsole(HttpContext context)
        {
            // Для логирования данных о запросе используем свойста объекта HttpContext
            Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
        }

        private void LogToDB(HttpContext context) 
        {
            Request request = new()
            {
                 Date = DateTime.Now,
                 Url = $"http://{context.Request.Host.Value + context.Request.Path}"
            };

            _repo.AddRequest(request);
        }
    }
}
