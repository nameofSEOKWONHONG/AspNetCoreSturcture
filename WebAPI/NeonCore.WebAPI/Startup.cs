using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Neon.Payment;
using Neon.Payment.BusinessLayer;
using Neon.Payment.BusinessLayer.Contract;
using NeonCore.BusinessLayer;
using NeonCore.BusinessLayer.Contract;
using NeonCore.Library;
using Newtonsoft.Json.Serialization;

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
            services.AddMvc()
                .AddJsonOptions(a => a.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            services.AddEntityFrameworkSqlServer().AddDbContext<JWDBContext>();
            services.AddEntityFrameworkSqlServer().AddDbContext<PaymentContext>();

            services.AddScoped<IUserInfo, UserInfo>();

            services.AddScoped<IUserBusinessObject, UserBusinessObject>();
            services.AddScoped<IOrderBusinessObject, OrderBusinessObject>();

            services.AddOptions();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddSingleton<IConfiguration>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            app.UseMvc();
        }
    }
}
