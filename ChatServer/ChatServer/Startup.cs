using System.IO.Compression;
using AutoMapper;
using ChatServer.Extentions;
using ChatServer.Hubs;
using ChatServer.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatServer
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                 builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });
            services.AddResponseCompression();
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            services.AddAutoMapper();
            services.ConfigureSqlServer(Configuration);
            services.ConfigureIdentity();
            services.ConfigureJwtTokens(Configuration);
            services.ConfigureServices();
            services.AddSignalR();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowAll");
            app.UseResponseCompression();
            app.UseAuthentication();
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Add("Cache-Control", "public,max-age=600");
                }
            });
            app.UseSignalR(routes => {
                routes.MapHub<ChatHub>("/chat");
            });
            app.UseMiddleware<ApiExceptionMiddleware>();
            app.UseMvc();
        }
    }
}
