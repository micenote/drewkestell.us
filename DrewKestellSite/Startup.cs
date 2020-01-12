using DrewKestellSite.Concerns;
using DrewKestellSite.Data;
using DrewKestellSite.Models;
using Ganss.XSS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DrewKestellSite
{
    public class Startup
    {
        readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression();
            services.AddAuthentication("KestellSiteAuthenticationScheme")
                 .AddCookie(
                     "KestellSiteAuthenticationScheme",
                     options =>
                     {
                         options.AccessDeniedPath = "/Home/AccessDenied/";
                         options.LoginPath = "/Admin/Session/Create/";
                     });

            services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddTransient<IAuthentication, Authentication>();

            services.Configure<Configuration.ApiConfiguration>(configuration);
            services.AddMvc();
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            var allowedCssClasses = new[] { "text-bold", "quote", "cite", "prettyprint", "img-memory", "img-memory-layout", "img-full", "img-hacked", "clearfix", "img-enough-talk", "ragh", "img-demonstration-1" };
            var sanitizer = new HtmlSanitizer(allowedCssClasses: allowedCssClasses);
            sanitizer.AllowedAttributes.Add("class");
            services.AddSingleton(typeof(HtmlSanitizer), sanitizer);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
