using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace NetCore.CorsPrototype.App.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCorsPolicies(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var origins = configuration.GetSection("AllowedOrigins").Get<string[]>();

            services.AddCors(corsOptions =>
            {
                corsOptions.AddPolicy(
                    name: "permissive",
                    configurePolicy: policyBuilder => policyBuilder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());

                corsOptions.AddPolicy(
                    name: "restrictive",
                    configurePolicy: policyBuilder => policyBuilder
                        .WithOrigins(origins ?? Array.Empty<string>())
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            return services;
        }

        public static IApplicationBuilder UseCorsPolicies(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors("permissive");
            }

            app.UseCors("restrictive");

            return app;
        }
    }
}
