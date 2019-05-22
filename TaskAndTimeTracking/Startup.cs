using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using TaskAndTimeTracking.Controller;
using TaskAndTimeTracking.Controller.interfaces;
using TaskAndTimeTracking.Persistence.Context;
using TaskAndTimeTracking.Persistence.Repository;
using TaskAndTimeTracking.Persistence.Repository.Interfaces;

namespace TaskAndTimeTracking
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
            services.AddMvc(config =>
            {
                //config.Filters.Add(new JwtAuthorizationFilter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            services.AddDbContext<ApplicationDatabaseContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DevDbLocal"));
            });
            
            // Authentication configuration for JSON Web Tokens
            services.AddAuthentication("JwtAuthHandler")
                .AddScheme<AuthenticationSchemeOptions, JwtAuthenticationHandler>("JwtAuthHandler", null);

            #region Repositories

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<IWorkProgressRepository, WorkProgressRepository>();

            #endregion
            
            
            #region Controllers

            services.AddScoped<IUserController, UserController>();
            services.AddScoped<IProjectController, ProjectController>();
            services.AddScoped<ITodoController, TodoController>();
            services.AddSingleton<IAuthControllerConfiguration>(s =>
            {
                return new AuthControllerConfiguration
                {
                    Secret = Configuration.GetValue<string>("SecurityConfig:JwtSecret"),
                    ExpirationDuration = Configuration.GetValue<int>("SecurityConfig:JwtExpirationTime")
                };
            });
            services.AddScoped<IAuthenticationController, AuthenticationController>();

            #endregion

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info {Title = "Task and Time Tracking System API", Version = "v1"});
                s.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    In = "header",
                    Description = "JWT Token in Bearer Scheme is expected",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                s.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }}
                });
            });
        }

        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(s =>
                {
                    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Task and Time Tracking System API V1");
                });
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}