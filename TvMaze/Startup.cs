using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using TvMaze.Data;
using TvMaze.Data.Repositories;
using TvMaze.ScheduledServices;
using TvMaze.Interfaces;
using TvMaze.Services;
using TvMaze.Data.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace TvMaze
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddDbContext<TvMazeContext>(opts =>
            {
                opts.UseSqlServer(Configuration.GetConnectionString("TvMazeConnectionString"));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("TVMazeAPI", new OpenApiInfo 
                { 
                    Title = "TvMaze API", 
                    Version = "1" 
                });
            });

            services
                .AddHttpClient<ITVMazeService, TVMazeService>(client =>
                {
                    client.BaseAddress = new Uri("http://api.tvmaze.com");
                })
                .AddPolicyHandler(PolicyHandler.WaitAndRetry())
                .AddPolicyHandler(PolicyHandler.Timeout());


            services.AddHostedService<TimerService>();
            services.AddScoped<ITVShowRepository, ShowRepository>();
            services.AddScoped<ICastmemberRepository, PersonRepository>();
            services.AddScoped<ICastShowLinkageRepository, ShowPersonRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/TVMazeAPI/swagger.json", "TvMaze API");
            });
        }
    }
}
