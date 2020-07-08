using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RegistroEstudiantes.Data;

namespace RegistroEstudiantes
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
            services.AddDbContextPool<RegistroEstudiantesContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("RegistroEstudiantesBD"));
            });

            //services.AddSingleton<IEstudianteService, InMemoryEstudiantesService>();
            //services.AddScoped<IEstudianteService, EFEstudiantes>();
            services.AddScoped<IEstudianteService, ADOEstudiantes>(sp =>
           {
               return new ADOEstudiantes(Configuration.GetConnectionString("RegistroEstudiantesBD"));
           
           });

            //services.AddSingleton<IMateriaService, inMemoryMateriasService>();
            // services.AddScoped<IMateriaService, EFMaterias>();

            services.AddScoped<IMateriaService, ADOMaterias>(sp =>  
            {
               return  new ADOMaterias(Configuration.GetConnectionString("RegistroEstudiantesBD"));


            });

            services.AddRazorPages();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
