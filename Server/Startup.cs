using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OpenCodeDev.NetCms.Server._NetCMS_.Api.Recipe.Controllers;
using OpenCodeDev.NetCms.Server.Api.Recipe.Controllers;
using OpenCodeDev.NetCms.Server.Database;
using ProtoBuf.Grpc.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenCodeDev.NetCMS.Generated.Api.Recipe.Model;
namespace OpenCodeDev.NetCms.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var t = new RecipesPublicModel();

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
            }));

            services.AddCodeFirstGrpc(config => { config.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal; });
            //TODO: In Memory, Only During Testing
            services.AddDbContext<ApiDatabase>(x => x.UseInMemoryDatabase("RecipeDatabase"), ServiceLifetime.Transient);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllers();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OpenCodeDev.CMS", Version = "v1" });
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OpenCodeDev.CMS v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseGrpcWeb(new GrpcWebOptions() { DefaultEnabled = true });
            app.UseCors();

            // EF CORE
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<ApiDatabase>();

                db.Database.EnsureCreated();
                db.MockUp();
                //PermissionExtension.GeneratePermissions();

                //db.EnsurePermissionsCreated(Configuration.GetSection("PermissionRoles").GetChildren().ToArray().Select(c => c.Value).ToArray());
            }
            
            
            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapGrpcService<RecipeControllerWrapper>().EnableGrpcWeb().RequireCors("AllowAll");
                    endpoints.MapControllers();
                });
        }
    }
}
