﻿//#define __JWT__
//#define __CUST_AUTH__

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Neon.Payment;
using Neon.Payment.BusinessLayer;
using Neon.Payment.BusinessLayer.Contract;
using NeonCore.BusinessLayer;
using NeonCore.BusinessLayer.Contract;
using NeonCore.Library;
using NeonCore.WebAPI.Authentication;
using NeonCore.WebAPI.Filters;
using NeonCore.WebAPI.Middleware;
using Newtonsoft.Json.Serialization;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace NeonCore.WebAPI
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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials()
                .Build());
            });

#if __JWT__
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Debug.WriteLine("auth failed : " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Debug.WriteLine("token validate : " + context.SecurityToken);
                        return Task.CompletedTask;
                    }
                };
            });
#endif

#if __CUST_AUTH__
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CustomAuthOptions.DefaultScheme;
                options.DefaultChallengeScheme = CustomAuthOptions.DefaultScheme;
            })
            .AddCustomAuth(options =>
            {
                options.AuthKey = "a123456789qwerty";
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(new AuthorizeFilter(new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));
            })
            .AddJsonOptions(a => a.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
#endif

            services.AddMvc().AddJsonOptions(a => a.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            services.AddEntityFrameworkSqlServer().AddDbContext<JWDBContext>();
            services.AddEntityFrameworkSqlServer().AddDbContext<PaymentContext>();
            
            services.AddScoped<IUserInfo, UserInfo>();

            services.AddScoped<IUserBusinessObject, UserBusinessObject>();
            services.AddScoped<IOrderBusinessObject, OrderBusinessObject>();

            services.AddScoped<LogFilter>();

            services.AddOptions();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddSingleton<IConfiguration>(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            loggerFactory.AddNLog();
            var logger = loggerFactory.CreateLogger<Startup>();

            app.AddNLogWeb();

            LogManager.Configuration.Variables["connectionString"] = Configuration.GetConnectionString("NLogDb");
            LogManager.Configuration.Variables["configDir"] = @"H:\github-new-mine\AspNetCoreSturcture\WebAPI\NeonCore.WebAPI\bin\Debug\netcoreapp2.0\Logs";

            app.UseCors("CorsPolicy");

            app.UseAuthentication();


            app.UseSampleMiddleware();

            //app.Run(async context =>
            //{
            //    logger.LogWarning($"In Run. During RUN, Order : {DateTime.Now}");
            //    await context.Response.WriteAsync($"In Run. Response from RUN. Order : {DateTime.Now}");
            //});

            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

        }
    }
}
