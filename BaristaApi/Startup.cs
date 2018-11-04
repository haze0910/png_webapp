using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Barista.SharedKernel;
using Barista.SharedKernel.Interfaces;
using CoffeeMgt.Core.Interfaces;
using CoffeeMgt.Infra.Data;
using CoffeeMgt.Infra.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StructureMap;

namespace BaristaApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // ===== Domain DB Contexts ========
            services.AddDbContext<CoffeeMgtDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SqlConnection")));
           
            services.AddMvc().AddControllersAsServices();
            
            

            var container = new Container();

            container.Configure(config =>
            {
                config.Scan(_ =>
                {
                    _.AssemblyContainingType(typeof(Startup)); // Web
                    _.AssemblyContainingType(typeof(BaseEntity)); // Core
                    _.Assembly("CoffeeMgt.Infra"); // Infrastructure
                    _.WithDefaultConventions();
                    
                });

                config.For<IDatabaseContext>().Use<CoffeeMgtDbContext>();
                config.For(typeof(IRepository<>)).Add(typeof(GenericRepository<>));
                config.For<IPantryRepository>().Use<PantryRepository>();


                //Populate the container using the service collection
                config.Populate(services);
            });

            return container.GetInstance<IServiceProvider>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

          
        }
    }
}
