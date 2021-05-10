using Covid19Chart.API.Hubs;
using Covid19Chart.API.Models;
using Covid19Chart.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Chart.API
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
            services.AddScoped<CovidService>(); //dependency injection ile diðer classlarda bu servisi constructor vasýtasýyla kullanabilmek için eklendi
            services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(Configuration["ConStr"]); //appsettings.jsondaki ConStr burada dahil edildi
            });

            //cors ekle
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => //CorsPolicy ne istersen o ismi ver
                {
                    builder.WithOrigins("https://localhost:44316").AllowAnyHeader().AllowAnyMethod().AllowCredentials(); //bu web url'e izin ver (web/mvc url)
                });
            });

            services.AddControllers();
            services.AddSignalR(); //eklendi
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy"); //auth üztüne bu eklenmeli

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<CovidHub>("/CovidHub"); //eklendi, huba bu url ile baðlanacak web tarafýnda .connection(... ile
            });
        }
    }
}
