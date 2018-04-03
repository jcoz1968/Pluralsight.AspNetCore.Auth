using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pluralsight.AspNetCore.Auth.Web.Interfaces;
using Pluralsight.AspNetCore.Auth.Web.Services;
using System.Collections.Generic;

namespace Pluralsight.AspNetCore.Auth.Web
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
			services.AddMvc(options => {
				options.Filters.Add(new RequireHttpsAttribute());
			});

			var users = new Dictionary<string, string> { { "coz", "password" } };
			services.AddSingleton<IUserService>(new DummyUserService(users));

			//services.AddAuthentication(options => {
			//	options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			//	options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			//	options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			//})
			//.AddCookie(options => { options.LoginPath = "/auth/signin"; })
			//.AddFacebook(fbopt => {
			//	fbopt.AppId = "229528624261290";
			//	fbopt.AppSecret = "a2140db463dd1d96edf4f0e51817279a";
			//});
			services.AddAuthentication(options => {
				options.DefaultChallengeScheme = FacebookDefaults.AuthenticationScheme;
				options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			})
			.AddFacebook(fbopt => {
				fbopt.AppId = "229528624261290";
				fbopt.AppSecret = "a2140db463dd1d96edf4f0e51817279a";
			})
			.AddCookie();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseRewriter(new RewriteOptions().AddRedirectToHttps(301, 44343));
			app.UseStaticFiles();
			app.UseAuthentication();
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
