using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Module32_MVC_Net5.Middleware;
using Module32_MVC_Net5.Models.Contexts;
using Module32_MVC_Net5.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Module32_MVC_Net5
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // регистрация сервиса репозитория ведения блога для взаимодействия с базой данных
            //И тут налетаю на ошибку "В этом контексте была начата вторая операция до завершения предыдущей операции" при частом обновлении представления
            //https://learn.microsoft.com/ru-ru/ef/core/dbcontext-configuration/#avoiding-dbcontext-threading-issues
            //Заменил Singleton на Scope
            //services.AddSingleton<IBlogRepository, BlogRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();

            // Задание 32.11.1 - Регистрация сервиса репозитория логгера для взаимодействия с базой данных
            //services.AddSingleton<ILoggerRepository, LoggerRepository>();
            services.AddScoped<ILoggerRepository, LoggerRepository>();

            string connection = Configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<BlogContext>(options => options.UseSqlServer(connection), ServiceLifetime.Singleton);
            services.AddDbContext<BlogContext>(options => options.UseSqlServer(connection),ServiceLifetime.Scoped);

            ////Задание 32.11.1 - Подключаю контекст для логирования в БД
            //services.AddDbContext<LoggerContext>(options => options.UseSqlServer(connection), ServiceLifetime.Singleton);

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Подключаем логирвоание с использованием ПО промежуточного слоя
            app.UseMiddleware<LoggingMiddleware>();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
