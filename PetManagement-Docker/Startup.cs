using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PetManager.Api.Data;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace PetManager.Api
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
            var somelist = new List<int>();
            services.AddDbContext<ApplicationDbContext>(options=>{
                options.UseMySql(Configuration.GetConnectionString("ApplicationData"), o=>{
                    o.EnableRetryOnFailure(50,TimeSpan.FromMinutes(10),somelist);
                });
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>{
                c.SwaggerDoc("v1",new OpenApiInfo{Title ="My Pet Manager Api",Description="An API for managing Pets", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            context.Database.Migrate();
            app.UseSwagger();
            app.UseSwaggerUI(option =>{
                option.SwaggerEndpoint("/swagger/v1/swagger.json","My Pet Api v1");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
