using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApiBiblioteca.Contexts;
using WebApiBiblioteca.Entities;
using WebApiBiblioteca.Entities.Helpers;

namespace WebApiBiblioteca
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
            //Aqui se está habilitando un conjunto de servicios para la funcionalidad de guardar información
            //en Caché
            services.AddResponseCaching();

            //Agregando servicio para el filtro de authorization
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer();

            //Ejemplo de Inyección de dependencias
            services.AddTransient<IClaseB, ClaseB2>();

            //agregando MiFiltroDeAccion
            services.AddScoped<MiFiltroDeAccion>();

            services.AddDbContext<ApplicationDBContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc(options =>
                   {
                       options.Filters.Add(new MiFiltroDeExcepcion());
                   }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            //configuración del middleware de cache
            app.UseResponseCaching();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
