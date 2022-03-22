using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using VehicleSales.Engine.UploadFile;
using VehicleSales.Engine.VehicleSales;
using VehicleSales.Infraestructure.Base.Context;
using VehicleSales.Infraestructure.Repos;
using VehicleSales.Model.Interfaces.Engine.File;
using VehicleSales.Model.Interfaces.Engine.VehicleSales;
using VehicleSales.Model.Interfaces.Repos.VehicleSales;

namespace VehicleSalesApi
{
    public class Startup
    {
        private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            AddSwagger(services);

            #region DbContext
            services.AddDbContext<VehicleSalesContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]);
            });
            #endregion

            services.AddScoped<IFileEngine, FileEngine>();
            services.AddScoped<IVehicleSalesEngine, VehicleSalesEngine>();
            services.AddScoped<IVehicleSalesRepo, VehicleSalesRepo>();

            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
            
            services.AddCors(opt =>
            {
                opt.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:4200");
                      });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vehicle Sales API V1");
            });

            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Vehicle Sales {groupName}",
                    Version = groupName,
                    Description = "Vehicle Sales API",
                    Contact = new OpenApiContact
                    {
                        Name = "Andres Duarte",
                        Email = "andresduarte152@gmail.com",
                        Url = new Uri("https://foo.com/"),
                    }
                });
            });
        }
    }
}
