using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using back.net_core.Models;
using back.net_core.Services;

namespace back.net_core
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Binding des paramètres avec le modèle InfoSettings 
            services.Configure<InfoSettings>(Configuration.GetSection("InfoSettings"));

            //Injection du contexte de données Accueil_visiteurs
            services.AddDbContext<AccueilContext>(options => options.UseSqlServer(Configuration.GetConnectionString("VisitsDatabase")));

            //Injection de la couche service
            services.AddScoped<IVisiteService,VisiteService>();

            services.AddApiVersioning();

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Création / Mise à jour de la base de données
            using (var serviceScope = app.ApplicationServices.CreateScope()){
                //Récupération du contexte de données
                var context = serviceScope.ServiceProvider.GetService<AccueilContext>();
                //appel de la mise à niveau de la base de données
                context.Database.Migrate();
            }

            //Activation de l'encapsulation des réponses pour les controleurs
            app.UseResponseWrapper();

            //Activation des CORS (selon l'environnement)
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseCors(builder => 
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
